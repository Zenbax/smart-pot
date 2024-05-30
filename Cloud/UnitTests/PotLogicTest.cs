
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
            var pot = new Pot { Id = "1" };
            var potDto = new PotCreationDto { Pot = pot };

            // Act
            var result = await _potLogic.CreatePot(potDto);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual("Create pot successfully", result.Message);
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
    }
}