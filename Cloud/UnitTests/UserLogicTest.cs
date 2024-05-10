    
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
            public async Task GetUsers_Returns_All_Users()
            {
                // Arrange
                var users = new List<User>
                {
                    new User { Id = "1", Email = "eq@da.dk", Password = "1234" },
                    new User { Id = "2", Email = "eq2@da.dk", Password = "1234" }

                };
                var mockCursor = new Mock<IAsyncCursor<User>>();
                mockCursor.Setup(_ => _.Current).Returns(users);
                mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(true))
                    .Returns(Task.FromResult(false));

                _mockUsersCollection.Setup(m => m.FindAsync<User>( // Async Find
                    It.IsAny<FilterDefinition<User>>(),
                    It.IsAny<FindOptions<User, User>>(),
                    It.IsAny<CancellationToken>()
                )).ReturnsAsync(mockCursor.Object);

                // Act
                var result = await _userLogic.GetUsers();

                // Assert

                Assert.IsNotNull(result);
                Assert.IsInstanceOf<IEnumerable<User>>(result);
                CollectionAssert.AreEqual(users, result);

            }
        }
    }