
namespace UnitTests
{
    [TestFixture]
    public class AuthLogicTests
    {
        private Mock<IMongoCollection<User>> _mockUsersCollection;
        private AuthLogic _authLogic;

        [SetUp]
        public void SetUp()
        {
            _mockUsersCollection = new Mock<IMongoCollection<User>>();
            _authLogic = new AuthLogic(_mockUsersCollection.Object, Mock.Of<ILogger<AuthLogic>>());
        }
        

        [Test]
        public async Task Login_UserDoesNotExist_ReturnsError()
        {
            // Arrange
            var user = new User { Email = "test@example.com", Password = "password" };
            var userLoginDto = new UserLoginDto { User = user };
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
            var result = await _authLogic.Login(userLoginDto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Invalid email or password.", result.Message);
            Assert.IsNull(result.User);
        }
    }
}
