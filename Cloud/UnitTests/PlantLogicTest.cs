using Moq;
using Application_.Logic;
using Domain.Model;
using Domain.DTOs;
using MongoDB.Driver;

namespace UnitTests
{
    [TestFixture]
    public class PlantLogicTests
    {
        private Mock<IMongoCollection<Plant>> _mockPlantsCollection;
        private PlantLogic _plantLogic;

        [SetUp]
        public void SetUp()
        {
            _mockPlantsCollection = new Mock<IMongoCollection<Plant>>();
            _plantLogic = new PlantLogic(_mockPlantsCollection.Object);
        }

        [Test]
        public async Task GetAllPlants_Returns_All_Plants()
        {
            // Arrange
            var plants = new List<Plant> { new Plant { NameOfPlant = "Plant1" }, new Plant { NameOfPlant = "Plant2" } };
            var mockFindFluent = new Mock<IAsyncCursor<Plant>>();
            mockFindFluent.SetupSequence(m => m.Current)
                .Returns(plants)
                .Returns(new List<Plant>());
            _mockPlantsCollection.Setup(m => m.Find(It.IsAny<FilterDefinition<Plant>>(), It.IsAny<FindOptions<Plant, Plant>>(), default)).ReturnsAsync(mockFindFluent.Object);

            // Act
            var result = await _plantLogic.GetAllPlants();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Plant>>(result);
            CollectionAssert.AreEqual(plants, result);
        }

        [Test]
        public async Task GetPlantByName_Returns_Single_Plant_When_Name_Exists()
        {
            // Arrange
            var plant = new Plant { NameOfPlant = "Plant1" };
            _mockPlantsCollection.Setup(m => m.Find(It.IsAny<FilterDefinition<Plant>>(), null, default)).ReturnsAsync(Mock.Of<Plant>(p => p.NameOfPlant == "Plant1"));

            // Act
            var result = await _plantLogic.GetPlantByName("Plant1");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Plant>(result);
            Assert.AreEqual(plant.NameOfPlant, result.NameOfPlant);
        }

        [Test]
        public async Task CreatePlant_Creates_New_Plant_And_Returns_Success_Message()
        {
            // Arrange
            var plantDto = new PlantCreationDto { NameOfPlant = "NewPlant", SoilMinimumMoisture = 0.5, ImageURL = "image.jpg" };

            // Act
            var result = await _plantLogic.CreatePlant(plantDto);

            // Assert
            Assert.AreEqual("Success", result);
            _mockPlantsCollection.Verify(m => m.InsertOneAsync(It.IsAny<Plant>(), null, default), Times.Once);
        }

        [Test]
        public async Task UpdatePlant_Updates_Existing_Plant_And_Returns_Success_Message()
        {
            // Arrange
            var updatedPlantDto = new PlantUpdateDto { SoilMinimumMoisture = 0.6, ImageUrl = "updated_image.jpg" };
            var existingPlant = new Plant { NameOfPlant = "ExistingPlant", SoilMinimumMoisture = 0.5, ImageUrl = "image.jpg" };
            _mockPlantsCollection.Setup(m => m.Find(It.IsAny<FilterDefinition<Plant>>(), null, default)).ReturnsAsync(existingPlant);

            // Act
            var result = await _plantLogic.UpdatePlant("ExistingPlant", updatedPlantDto);

            // Assert
            Assert.AreEqual("Success", result);
            _mockPlantsCollection.Verify(m => m.ReplaceOneAsync(It.IsAny<FilterDefinition<Plant>>(), It.IsAny<Plant>(), null, default), Times.Once);
        }

        [Test]
        public async Task DeletePlant_Removes_Existing_Plant_And_Returns_Success_Message()
        {
            // Arrange

            // Act
            var result = await _plantLogic.DeletePlant("PlantToDelete");

            // Assert
            Assert.AreEqual("Success", result);
            _mockPlantsCollection.Verify(m => m.DeleteOneAsync(It.IsAny<FilterDefinition<Plant>>(), null, default), Times.Once);
        }
    }
}