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

          
        }
    }