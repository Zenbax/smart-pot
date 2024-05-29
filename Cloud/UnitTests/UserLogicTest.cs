
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
            _userLogic = new UserLogic(_mockUsersCollection.Object, Mock.Of<ILogger<UserLogic>>());
        }
        
        [Test]
        public async Task GetUserById_UserDoesNotExist_ReturnsError()
        {
            // Arrange
            var userId = "1";
            var userDto = new UserGetByIdDto { IdToGet = userId };
            var mockAsyncCursor = new Mock<IAsyncCursor<User>>();
            mockAsyncCursor.Setup(_ => _.Current).Returns(new List<User>());
            mockAsyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _mockUsersCollection.Setup(x =>
                    x.FindAsync(It.IsAny<FilterDefinition<User>>(), It.IsAny<FindOptions<User, User>>(), default))
                .ReturnsAsync(mockAsyncCursor.Object);

            // Act
            var result = await _userLogic.GetUserById(userDto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("User with id 1 not found.", result.Message);
            Assert.IsNull(result.User);
        }

        [Test]
        public async Task GetUsers_NoUsersInDatabase_ReturnsNoUsersMessage()
        {
            // Arrange
            var users = new List<User>();
            var mockAsyncCursor = new Mock<IAsyncCursor<User>>();
            mockAsyncCursor.Setup(_ => _.Current).Returns(users);
            mockAsyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _mockUsersCollection.Setup(x =>
                    x.FindAsync(It.IsAny<FilterDefinition<User>>(), It.IsAny<FindOptions<User, User>>(), default))
                .ReturnsAsync(mockAsyncCursor.Object);

            // Act
            var result = await _userLogic.GetUsers();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Users);
            Assert.AreEqual("No users in database.", result.Message);
        }


        [Test]
        public async Task UpdateUser_UserDoesNotExist_ReturnsError()
        {
            // Arrange
            var userId = "1";
            var userDto = new UserUpdateDto
            {
                IdToUpdate = userId,
                User = new User
                {
                    Name = "Jane", LastName = "Smith", Email = "jane@example.com", Password = "password",
                    PhoneNumber = "1234567890"
                }
            };
            var mockAsyncCursor = new Mock<IAsyncCursor<User>>();
            mockAsyncCursor.Setup(_ => _.Current).Returns(new List<User>());
            mockAsyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _mockUsersCollection.Setup(x =>
                    x.FindAsync(It.IsAny<FilterDefinition<User>>(), It.IsAny<FindOptions<User, User>>(), default))
                .ReturnsAsync(mockAsyncCursor.Object);

            // Act
            var result = await _userLogic.UpdateUser(userDto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("User with id 1 not found.", result.Message);
            Assert.IsNull(result.User);
        }
    }
}
