using YourApiNamespace.Controllers;

namespace UnitTests
{
    [TestFixture]
    public class PotLogicTest
    {
    private Mock<IMongoCollection<Pot>> _mockPotsCollection;
        private Mock<IMongoCollection<SensorData>> _mockSensorDataCollection;
        private PotLogic _potLogic;

        [SetUp]
        public void Setup()
        {
            _mockPotsCollection = new Mock<IMongoCollection<Pot>>();
            _mockSensorDataCollection = new Mock<IMongoCollection<SensorData>>();
            _potLogic = new PotLogic(_mockPotsCollection.Object, _mockSensorDataCollection.Object);
        }
        
        [Test]
        public async Task CreatePot_SuccessfullyCreatesPot()
        {
            // Arrange
            var potDto = new PotCreationDto { Pot = new Pot { NameOfPot = "Pot1", MachineID = "123", Enable = 1 } };
            var mockAsyncCursor = new Mock<IAsyncCursor<Pot>>();
            mockAsyncCursor.Setup(_ => _.Current).Returns(new List<Pot>());
            mockAsyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _mockPotsCollection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<Pot>>(), It.IsAny<FindOptions<Pot, Pot>>(), default))
                .ReturnsAsync(mockAsyncCursor.Object);

            // Act
            var result = await _potLogic.CreatePot(potDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Pot created successfully.", result.Message);
        }
        
        
        [Test]
        public async Task DeletePot_PotExists_SuccessfullyDeletesPot()
        {
            // Arrange
            var potId = "1";
            var potDto = new PotDeleteDto { IdToDelete = potId };
            var deleteResult = Mock.Of<DeleteResult>(x => x.DeletedCount == 1);
            _mockPotsCollection.Setup(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<Pot>>(), default))
                .ReturnsAsync(deleteResult);

            // Act
            var result = await _potLogic.DeletePot(potDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Pot deleted successfully", result.Message);
        }
        
        [Test]
        public async Task DeletePot_PotDoesNotExist_ReturnsError()
        {
            // Arrange
            var potId = "1";
            var potDto = new PotDeleteDto { IdToDelete = potId };
            var deleteResult = Mock.Of<DeleteResult>(x => x.DeletedCount == 0);
            _mockPotsCollection.Setup(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<Pot>>(), default))
                .ReturnsAsync(deleteResult);

            // Act
            var result = await _potLogic.DeletePot(potDto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Pot not found", result.Message);
        }
       
        [Test]
        public async Task GetPotById_PotDoesNotExist_ReturnsError()
        {
            // Arrange
            var potId = "1";
            var potDto = new PotGetByIdDto { IdToGet = potId };
            var mockAsyncCursor = new Mock<IAsyncCursor<Pot>>();
            mockAsyncCursor.Setup(_ => _.Current).Returns(new List<Pot>());
            mockAsyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _mockPotsCollection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<Pot>>(), It.IsAny<FindOptions<Pot, Pot>>(), default))
                .ReturnsAsync(mockAsyncCursor.Object);

            // Act
            var result = await _potLogic.GetPotById(potDto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Pot not found", result.Message);
            Assert.IsNull(result.Pot);
        }
        
        [Test]
        public async Task UpdatePot_PotDoesNotExist_ReturnsError()
        {
            // Arrange
            var potId = "1";
            var potDto = new PotUpdateDto { IdToUpdate = potId, Pot = new Pot { NameOfPot = "UpdatedPot", MachineID = "456", Enable = 1 } };
            var mockAsyncCursor = new Mock<IAsyncCursor<Pot>>();
            mockAsyncCursor.Setup(_ => _.Current).Returns(new List<Pot>());
            mockAsyncCursor
                .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
                .Returns(true)
                .Returns(false);
            _mockPotsCollection.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<Pot>>(), It.IsAny<FindOptions<Pot, Pot>>(), default))
                .ReturnsAsync(mockAsyncCursor.Object);

            // Act
            var result = await _potLogic.UpdatePot(potDto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Pot with ID1 not found", result.Message);
            Assert.IsNull(result.Pot);
        }
        
        [Test]
        public async Task DeletePot_Returns_Pot_Not_Found_When_Exception_Is_Thrown()
        {
            // Arrange
            var potId = "1";
            var potDto = new PotDeleteDto { IdToDelete = potId };
            _mockPotsCollection.Setup(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<Pot>>(), default))
                .Throws(new Exception());

            // Act
            var result = await _potLogic.DeletePot(potDto);

            // Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Error: Exception of type 'System.Exception' was thrown.", result.Message);
        }
    }
}