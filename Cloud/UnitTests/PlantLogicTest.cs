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
/*
        [Test]
        public async Task GetAllPlants_Returns_All_Plants()
        {
            // Arrange
            var mockPlantCollection = new Mock<IMongoCollection<Plant>>();
            var mockPlantCollectionSetup = mockPlantCollection.Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<FindOptions<Plant, Plant>>(),
                It.IsAny<CancellationToken>()));

            var plants = new List<Plant>
            {
                new Plant
                {
                    Id = "1",
                    NameOfPlant = "Plant1",
                    SoilMinimumMoisture = 12,
                    AmountOfWaterToBeGiven = 12,
                    Image = "https://www.google.com",
                    isDefault = false,
                    UserId = "1",   
                },
                new Plant
                {
                    Id = "2",
                    NameOfPlant = "Plant2",
                    SoilMinimumMoisture = 12,
                    AmountOfWaterToBeGiven = 12,
                    Image = "https://www.google.com",
                    isDefault = false,
                    UserId = "1",
                }
            };

            var mockCursor = new Mock<IAsyncCursor<Plant>>();
            mockCursor.Setup(_ => _.Current).Returns(plants);
            mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

            mockPlantCollectionSetup.Returns(Task.FromResult(mockCursor.Object));

            var plantLogic = new PlantLogic(mockPlantCollection.Object);

            // Act

            var result = await plantLogic.GetAllPlants();

            // Assert
            Assert.AreEqual(plants.Count, result.Plants.Count);
            Assert.AreEqual(plants[0].NameOfPlant, result.Plants.First().NameOfPlant);
            Assert.AreEqual(plants[1].NameOfPlant, result.Plants.Last().NameOfPlant);
        }
        */
        
        [Test]
        public async Task GetPlantByName_Returns_Plant_With_Name()
        {
            // Arrange
            var mockPlantCollection = new Mock<IMongoCollection<Plant>>();
            var mockPlantCollectionSetup = mockPlantCollection.Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<FindOptions<Plant, Plant>>(),
                It.IsAny<CancellationToken>()));

            var plants = new List<Plant>
            {
                new Plant
                {
                    Id = "1",
                    NameOfPlant = "Plant1",
                    SoilMinimumMoisture = 12,
                    AmountOfWaterToBeGiven = 12,
                    Image = "https://www.google.com"
                },
                new Plant
                {
                    Id = "2",
                    NameOfPlant = "Plant2",
                    SoilMinimumMoisture = 12,
                    AmountOfWaterToBeGiven = 12,
                    Image = "https://www.google.com"
                }
            };

            var mockCursor = new Mock<IAsyncCursor<Plant>>();
            mockCursor.Setup(_ => _.Current).Returns(plants);
            mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

            mockPlantCollectionSetup.Returns(Task.FromResult(mockCursor.Object));

            var plantLogic = new PlantLogic(mockPlantCollection.Object);

            var plantGetByNameDto = new PlantGetByNameDto
            {
                NameToGet = "Plant1"
            };

            // Act
            var result = await plantLogic.GetPlantByName(plantGetByNameDto);

            // Assert
            Assert.AreEqual(plants[0].NameOfPlant, result.Plant.NameOfPlant);
        }
        
        [Test]
        public async Task CreatePlant_Returns_Success()
        {
            // Arrange
            var mockPlantCollection = new Mock<IMongoCollection<Plant>>();
            var mockPlantCollectionSetup = mockPlantCollection.Setup(c => c.InsertOneAsync(
                It.IsAny<Plant>(),
                It.IsAny<InsertOneOptions>(),
                It.IsAny<CancellationToken>()));

            var plant = new Plant
            {
                Id = "1",
                NameOfPlant = "Plant1",
                SoilMinimumMoisture = 12,
                AmountOfWaterToBeGiven = 12,
                Image = "https://www.google.com"
            };

            mockPlantCollectionSetup.Returns(Task.CompletedTask);

            var plantLogic = new PlantLogic(mockPlantCollection.Object);

            var plantCreationDto = new PlantCreationDto
            {
                Plant = plant
            };

            // Act
            var result = await plantLogic.CreatePlant(plantCreationDto);

            // Assert
            Assert.IsTrue(result.Success);
        }
        
        [Test]
        public async Task UpdatePlant_Returns_Failure()
        {
            // Arrange
            var mockPlantCollection = new Mock<IMongoCollection<Plant>>();
            var mockPlantCollectionSetup = mockPlantCollection.Setup(c => c.FindOneAndUpdateAsync(
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<UpdateDefinition<Plant>>(),
                It.IsAny<FindOneAndUpdateOptions<Plant, Plant>>(),
                It.IsAny<CancellationToken>()));

            mockPlantCollectionSetup.Returns(Task.FromResult((Plant)null));

            var plantLogic = new PlantLogic(mockPlantCollection.Object);

            var plantUpdateDto = new PlantUpdateDto
            {
                NameToUpdate = "Plant1",
                Plant = new Plant
                {
                    Id = "1",
                    NameOfPlant = "Plant1",
                    SoilMinimumMoisture = 12,
                    AmountOfWaterToBeGiven = 12,
                    Image = "https://www.google.com",
                    UserId = "1",
                    isDefault = false
                }
            };

            // Act
            var result = await plantLogic.UpdatePlant(plantUpdateDto);

            // Assert
            Assert.IsFalse(result.Success);
        }
        
        [Test]
        public async Task DeletePlant_Returns_Failure()
        {
            // Arrange
            var mockPlantCollection = new Mock<IMongoCollection<Plant>>();
            var mockPlantCollectionSetup = mockPlantCollection.Setup(c => c.DeleteOneAsync(
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<CancellationToken>()));

            mockPlantCollectionSetup.Throws(new Exception());

            var plantLogic = new PlantLogic(mockPlantCollection.Object);

            var plantDeleteDto = new PlantDeleteDto
            {
                NameToDelete = "Plant1"
            };

            // Act
            var result = await plantLogic.DeletePlant(plantDeleteDto);

            // Assert
            Assert.IsFalse(result.Success);
        }
        
        [Test]
        public async Task DeletePlant_Returns_Failure_When_Exception_Is_Thrown()
        {
            // Arrange
            var mockPlantCollection = new Mock<IMongoCollection<Plant>>();
            var mockPlantCollectionSetup = mockPlantCollection.Setup(c => c.DeleteOneAsync(
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<CancellationToken>()));

            mockPlantCollectionSetup.Throws(new Exception());

            var plantLogic = new PlantLogic(mockPlantCollection.Object);

            var plantDeleteDto = new PlantDeleteDto
            {
                NameToDelete = "Plant1"
            };

            // Act
            var result = await plantLogic.DeletePlant(plantDeleteDto);

            // Assert
            Assert.IsFalse(result.Success);
        }
        
        [Test]
        public async Task DeletePlant_Returns_Failure_When_Plant_Is_Not_Found()
        {
            // Arrange
            var mockPlantCollection = new Mock<IMongoCollection<Plant>>();
            var mockPlantCollectionSetup = mockPlantCollection.Setup(c => c.DeleteOneAsync(
                It.IsAny<FilterDefinition<Plant>>(),
                It.IsAny<CancellationToken>()));

            mockPlantCollectionSetup.Returns(Task.FromResult((DeleteResult)null));

            var plantLogic = new PlantLogic(mockPlantCollection.Object);

            var plantDeleteDto = new PlantDeleteDto
            {
                NameToDelete = "Plant1"
            };

            // Act
            var result = await plantLogic.DeletePlant(plantDeleteDto);

            // Assert
            Assert.IsFalse(result.Success);
        }
    }
}  
