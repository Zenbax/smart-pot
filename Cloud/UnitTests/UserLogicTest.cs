using Moq;
using Application_.Logic;
using Domain.Model;
using Domain.DTOs;
using MongoDB.Driver;

namespace UnitTests
{
    [TestFixture]
    public class UserLogicTests
    {
        private Mock<IMongoCollection<User>> _mockUsersCollection;
        private UserLogic _userLogic;

        [SetUp]
        public void SetUp()
        {
            _mockUsersCollection = new Mock<IMongoCollection<User>>();
            _userLogic = new UserLogic(_mockUsersCollection.Object);
        }

        [Test]
        public async Task Login_Returns_Token_When_User_Exists()
        {
            // Arrange
            var userLoginDto = new LoginDto { Username = "test@example.com", Password = "password" };
            var existingUser = new User { Email = "test@example.com", Password = "password" };
            _mockUsersCollection.Setup(m => m.Find(It.IsAny<FilterDefinition<User>>(), null, default)).ReturnsAsync(existingUser);

            // Act
            var result = await _userLogic.Login(userLoginDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<string>(result);
        }

        [Test]
        public async Task Login_Returns_Null_When_User_Does_Not_Exist()
        {
            // Arrange
            var userLoginDto = new LoginDto { Username = "test@example.com", Password = "password" };
            _mockUsersCollection.Setup(m => m.Find(It.IsAny<FilterDefinition<User>>(), null, default)).ReturnsAsync((User)null);

            // Act
            var result = await _userLogic.Login(userLoginDto);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task Register_Creates_New_User_And_Returns_Id()
        {
            // Arrange
            var userCreationDto = new UserCreationDto { Name = "John", LastName = "Doe", Email = "john@example.com", Password = "password", PhoneNumber = "123456789" };
            _mockUsersCollection.Setup(m => m.Find(It.IsAny<FilterDefinition<User>>(), It.IsAny<FindOptions<User, User>>(), default)).ReturnsAsync((User)null);

            // Act
            var result = await _userLogic.Register(userCreationDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<string>(result);
        }

        [Test]
        public async Task Register_Throws_Exception_When_Email_Already_Exists()
        {
            // Arrange
            var userCreationDto = new UserCreationDto { Name = "John", LastName = "Doe", Email = "john@example.com", Password = "password", PhoneNumber = "123456789" };
            var existingUser = new User { Email = "john@example.com" };
            _mockUsersCollection.Setup(m => m.Find(It.IsAny<FilterDefinition<User>>(), null, default)).ReturnsAsync(existingUser);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => _userLogic.Register(userCreationDto));
        }

        [Test]
        public async Task GetUserById_Returns_User_When_Id_Exists()
        {
            // Arrange
            var userId = "1";
            var existingUser = new User { Id = "1", Name = "John", LastName = "Doe", Email = "john@example.com", Password = "password", PhoneNumber = "123456789" };
            _mockUsersCollection.Setup(m => m.Find(It.IsAny<FilterDefinition<User>>(), null, default)).ReturnsAsync(existingUser);

            // Act
            var result = await _userLogic.GetUserById(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(existingUser, result);
        }

        [Test]
        public async Task GetUserById_Returns_Null_When_Id_Does_Not_Exist()
        {
            // Arrange
            var userId = "1";
            _mockUsersCollection.Setup(m => m.Find(It.IsAny<FilterDefinition<User>>(), null, default)).ReturnsAsync((User)null);

            // Act
            var result = await _userLogic.GetUserById(userId);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetUsers_Returns_All_Users()
        {
            // Arrange
            var users = new List<User> { new User { Id = "1", Name = "John", LastName = "Doe", Email = "john@example.com", Password = "password", PhoneNumber = "123456789" }, new User { Id = "2", Name = "Jane", LastName = "Doe", Email = "jane@example.com", Password = "password", PhoneNumber = "987654321" } };
            var mockFindFluent = new Mock<IAsyncCursor<User>>();
            mockFindFluent.Setup(m => m.Current).Returns(users);
            _mockUsersCollection.Setup(m => m.Find(It.IsAny<FilterDefinition<User>>(), It.IsAny<FindOptions<User, User>>(), default)).Returns(mockFindFluent.Object);

            // Act
            var result = await _userLogic.GetUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<User>>(result);
            CollectionAssert.AreEqual(users, result);
        }
    }
}
