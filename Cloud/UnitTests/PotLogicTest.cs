﻿using Moq;
using Application_.Logic;
using Domain.Model;
using Domain.DTOs;
using MongoDB.Driver;
using YourApiNamespace.Controllers;

namespace UnitTests
{
    [TestFixture]
    public class PotLogicTest
    {
        private Mock<IMongoCollection<Pot>> _mockPotsCollection;
        private PotLogic _potLogic;

        [SetUp]
        public void SetUp()
        {
            _mockPotsCollection = new Mock<IMongoCollection<Pot>>();
            _potLogic = new PotLogic(_mockPotsCollection.Object);
        }


        [Test]
        public async Task GetAllPots_Returns_All_Pots()
        {
            // Arrange
            var mockPotCollection = new Mock<IMongoCollection<Pot>>();
            var mockPlantCollection = new Mock<IMongoCollection<Plant>>();

            var pots = new List<Pot>
            {
                new Pot
                {
                    Id = "1",
                    NameOfPot = "Pot1",
                    SoilMoisturePercent = 50,
                    WaterReservoirPercent = 60,
                    WateringLog = new List<HumidityLog>(),
                    MoistureLog = new List<HumidityLog>()
                },
                new Pot
                {
                    Id = "2",
                    NameOfPot = "Pot2",
                    SoilMoisturePercent = 60,
                    WaterReservoirPercent = 70,
                    WateringLog = new List<HumidityLog>(),
                    MoistureLog = new List<HumidityLog>()
                }
            };

            var mockCursor = new Mock<IAsyncCursor<Pot>>();
            mockCursor.Setup(_ => _.Current).Returns(pots);
            mockCursor.SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

            var mockPotCollectionSetup = mockPotCollection.Setup(c => c.FindAsync(
                It.IsAny<FilterDefinition<Pot>>(),
                It.IsAny<FindOptions<Pot, Pot>>(),
                It.IsAny<CancellationToken>()));

            mockPotCollectionSetup.Returns(Task.FromResult(mockCursor.Object));

            var potLogic = new PotLogic(mockPotCollection.Object);

            // Act
            var result = await potLogic.GetAllPots();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Pot>>(result);
            Assert.AreEqual(2, result.Count());
        }
    

        [Test]
        public async Task GetPotById_Returns_Null_When_Id_Does_Not_Exist()
        {
            // Arrange
            var potId = "1";
            _mockPotsCollection.Setup(m => m.FindAsync<Pot>( // Async Find
                It.IsAny<FilterDefinition<Pot>>(),
                It.IsAny<FindOptions<Pot, Pot>>(),
                It.IsAny<CancellationToken>()
            )).ReturnsAsync(Mock.Of<IAsyncCursor<Pot>>(x => x.Current == null));

            // Act
            var result = await _potLogic.GetPotById(potId);

            // Assert
            Assert.IsNull(result);
        }
        
        [Test]
        public async Task CreatePot_Creates_New_Pot_And_Returns_Success_Message()
        {
            // Arrange
            var potDto = new PotCreationDto { Name = "NewPot", HumidityPercent = 50, ImageUrl = "image.jpg" };

            // Act
            var result = await _potLogic.CreatePot(potDto);

            // Assert
            Assert.AreEqual("Success", result);
            _mockPotsCollection.Verify(m => m.InsertOneAsync(It.IsAny<Pot>(), null, default), Times.Once);
        }
        
        
        
        
        [Test]
        public async Task CreatePot_Returns_Error_Message_When_Exception_Is_Thrown()
        {
            // Arrange
            var potDto = new PotCreationDto { Name = "NewPot", HumidityPercent = 50, ImageUrl = "image.jpg" };
            _mockPotsCollection.Setup(m => m.InsertOneAsync(It.IsAny<Pot>(), null, default)).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _potLogic.CreatePot(potDto);

            // Assert
            Assert.AreEqual("Error: An error occurred", result);
        }
        
        
        
        [Test]
        public async Task DeletePot_Returns_Error_Message_When_Exception_Is_Thrown()
        {
            // Arrange
            var potId = "1";
            _mockPotsCollection.Setup(m => m.DeleteOneAsync(
                It.IsAny<FilterDefinition<Pot>>(),
                It.IsAny<CancellationToken>()
            )).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _potLogic.DeletePot(potId);

            // Assert
            Assert.AreEqual("Error: An error occurred", result);
        }
        
        
        [Test]
        public async Task GetPotById_Returns_Error_Message_When_Exception_Is_Thrown()
        {
            // Arrange
            var potId = "1";
            _mockPotsCollection.Setup(m => m.FindAsync<Pot>(
                It.IsAny<FilterDefinition<Pot>>(),
                It.IsAny<FindOptions<Pot, Pot>>(),
                It.IsAny<CancellationToken>()
            )).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _potLogic.GetPotById(potId);

            // Assert
            Assert.AreEqual(null, result);
        }
        
        
        [Test]
        public async Task DeletePot_Returns_Pot_Not_Found_When_Exception_Is_Thrown()
        {
            // Arrange
            var potId = "1";
            _mockPotsCollection.Setup(m => m.DeleteOneAsync(
                It.IsAny<FilterDefinition<Pot>>(),
                It.IsAny<CancellationToken>()
            )).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _potLogic.DeletePot(potId);

            // Assert
            Assert.AreEqual("Error: An error occurred", result);
        }
    }
}