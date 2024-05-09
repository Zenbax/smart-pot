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
        public async Task CreatePlant_Creates_New_Plant_And_Returns_Success_Message()
        {
            // Arrange
            var plantDto = new PlantCreationDto { NameOfPlant = "NewPlant", SoilMinimumMoisture = 50, ImageUrl = "image.jpg" };

            // Act
            var result = await _plantLogic.CreatePlant(plantDto);

            // Assert
            Assert.AreEqual("Success", result);
            _mockPlantsCollection.Verify(m => m.InsertOneAsync(It.IsAny<Plant>(), null, default), Times.Once);
        }
        
        [Test]
        public async Task GetPlantByName_Returns_Null_When_Name_Does_Not_Exist()
        {
            // Arrange
            var plantName = "Plant1";
            _mockPlantsCollection.Setup(m => m.FindAsync<Plant>( // Async Find
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<FindOptions<Plant, Plant>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(Mock.Of<IAsyncCursor<Plant>>(x => x.Current == null));

            // Act
            var result = await _plantLogic.GetPlantByName(plantName);

            // Assert
            Assert.IsNull(result);
        }
        
        [Test]
        public async Task UpdatePlant_Returns_Plant_Not_Found_When_Name_Does_Not_Exist()
        {
            // Arrange
            var updatedPlantDto = new PlantUpdateDto { SoilMinimumMoisture = 50, ImageURL = "updated_image.jpg" };
            var existingPlant = new Plant { NameOfPlant = "ExistingPlant", SoilMinimumMoisture = 50, ImageUrl = "image.jpg" };
            _mockPlantsCollection.Setup(m => m.FindAsync<Plant>( // Async Find
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<FindOptions<Plant, Plant>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(Mock.Of<IAsyncCursor<Plant>>(x => x.Current == null));

            // Act
            var result = await _plantLogic.UpdatePlant(existingPlant.NameOfPlant, updatedPlantDto);

            // Assert
            Assert.AreEqual("Plant not found", result);
        }
        
        [Test]
        public async Task DeletePlant_Returns_Plant_Not_Found_When_Name_Does_Not_Exist()
        {
            // Arrange
            var plantNameToDelete = "PlantToDelete";
            _mockPlantsCollection.Setup(m => m.FindAsync<Plant>( // Async Find
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<FindOptions<Plant, Plant>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(Mock.Of<IAsyncCursor<Plant>>(x => x.Current == null));

            // Act
            var result = await _plantLogic.DeletePlant(plantNameToDelete);

            // Assert
            Assert.AreEqual("Plant not found", result);
        }
        
        [Test]
        public async Task CreatePlant_Returns_Error_Message_When_Exception_Occurs()
        {
            // Arrange
            var plantDto = new PlantCreationDto { NameOfPlant = "NewPlant", SoilMinimumMoisture = 50, ImageUrl = "image.jpg" };
            _mockPlantsCollection.Setup(m => m.InsertOneAsync(It.IsAny<Plant>(), null, default)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _plantLogic.CreatePlant(plantDto);

            // Assert
            Assert.AreEqual("Error: Error", result);
        }
        
        [Test]
        public async Task GetPlantByName_Returns_Error_Message_When_Exception_Occurs()
        {
            // Arrange
            var plantName = "Plant1";
            _mockPlantsCollection.Setup(m => m.FindAsync<Plant>( // Async Find
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<FindOptions<Plant, Plant>>(),
                It.IsAny<CancellationToken>()
            )).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _plantLogic.GetPlantByName(plantName);

            // Assert
            Assert.AreEqual(null, result);
        }
        
        [Test]
        public async Task GetAllPlants_Returns_Error_Message_When_Exception_Occurs()
        {
            // Arrange
            _mockPlantsCollection.Setup(m => m.FindAsync<Plant>( // Async Find
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<FindOptions<Plant, Plant>>(),
                It.IsAny<CancellationToken>()
            )).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _plantLogic.GetAllPlants();

            // Assert
            Assert.AreEqual(result, null);
        }
        [Test]
        public async Task GetPlantByName_Returns_Single_Plant_When_Name_Exists()
        {
            // Arrange
            var plantName = "Plant1";
            var plant = new Plant { NameOfPlant = plantName };
            _mockPlantsCollection.Setup(m => m.FindAsync<Plant>( // Async Find
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<FindOptions<Plant, Plant>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(Mock.Of<IAsyncCursor<Plant>>(x => x.Current == new List<Plant> { plant }));

            // Act
            var result = await _plantLogic.GetPlantByName(plantName);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Plant>(result);
            Assert.AreEqual(plantName, result.NameOfPlant);
        }

        [Test]
        public async Task GetAllPlants_Returns_All_Plants()
        {
            // Arrange
            var plants = new List<Plant> { new Plant { NameOfPlant = "Plant1" }, new Plant { NameOfPlant = "Plant2" } };
            var mockFindFluent = new Mock<IAsyncCursor<Plant>>();
            mockFindFluent.SetupSequence(m => m.Current)
                .Returns(plants);

            _mockPlantsCollection.Setup(m => m.FindAsync<Plant>( // Async Find
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<FindOptions<Plant, Plant>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(mockFindFluent.Object);

            // Act
            var result = await _plantLogic.GetAllPlants();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Plant>>(result);
            CollectionAssert.AreEqual(plants, result);
        }
    }
}
