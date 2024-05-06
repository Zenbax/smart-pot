// using Moq;
// using Application_.Logic;
// using Domain.Model;
// using Domain.DTOs;
// using MongoDB.Driver;
// using YourApiNamespace.Controllers;
//
// namespace UnitTests
// {
//     [TestFixture]
//     public class PotLogicTest
//     {
//         private Mock<IMongoCollection<Pot>> _mockPotsCollection;
//         private PotLogic _potLogic;
//
//         [SetUp]
//         public void SetUp()
//         {
//             _mockPotsCollection = new Mock<IMongoCollection<Pot>>();
//             _potLogic = new PotLogic(_mockPotsCollection.Object);
//         }
//
//         [Test]
//         public async Task GetAllPots_Returns_All_Pots()
//         {
//             // Arrange
//             var pots = new List<Pot>
//                 { new Pot { Id = "1", NameOfPot = "Pot1" }, new Pot { Id = "2", NameOfPot = "Pot2" } };
//             var mockFindFluent = new Mock<IAsyncCursor<Pot>>();
//             mockFindFluent.Setup(m => m.Current).Returns(pots);
//             _mockPotsCollection
//                 .Setup(m => m.Find(It.IsAny<FilterDefinition<Pot>>(), It.IsAny<FindOptions<Pot, Pot>>(), default))
//                 .Returns(mockFindFluent.Object);
//
//             // Act
//             var result = await _potLogic.GetAllPots();
//
//             // Assert
//             Assert.IsNotNull(result);
//             Assert.IsInstanceOf<IEnumerable<Pot>>(result);
//             CollectionAssert.AreEqual(pots, result);
//         }
//
//         [Test]
//         public async Task GetPotById_Returns_Single_Pot_When_Id_Exists()
//         {
//             // Arrange
//             var pot = new Pot { Id = "1", NameOfPot = "Pot1" };
//             _mockPotsCollection.Setup(m => m.Find(p => p.Id == "1", null, default)).ReturnsAsync(pot);
//
//             // Act
//             var result = await _potLogic.GetPotById("1");
//
//             // Assert
//             Assert.IsNotNull(result);
//             Assert.IsInstanceOf<Pot>(result);
//             Assert.AreEqual(pot, result);
//         }
//
//         [Test]
//         public async Task CreatePot_Creates_New_Pot_And_Returns_Success_Message()
//         {
//             // Arrange
//             var potDto = new PotCreationDto
//             {
//                 NameOfPot = "NewPot", NameOfPlant = "Plant1", SoilMinimumMoisture = 0.5, HumidityPercent = 50,
//                 ImageUrl = "image.jpg"
//             };
//
//             // Act
//             var result = await _potLogic.CreatePot(potDto);
//
//             // Assert
//             Assert.AreEqual("Success", result);
//             _mockPotsCollection.Verify(m => m.InsertOneAsync(It.IsAny<Pot>(), null, default), Times.Once);
//         }
//
//         [Test]
//         public async Task UpdatePot_Updates_Existing_Pot_And_Returns_Success_Message()
//         {
//             // Arrange
//             var potUpdatedDto = new PotUpdatedDto
//             {
//                 Name = "UpdatedPot", NameOfPlant = "UpdatedPlant", SoilMinimumMoisture = 0.6, HumidityPercent = 60,
//                 ImageUrl = "updated_image.jpg"
//             };
//             var existingPot = new Pot
//             {
//                 Id = "1", NameOfPot = "ExistingPot", Plant = new Plant { NameOfPlant = "ExistingPlant" },
//                 SoilMoisturePercent = 50
//             };
//             _mockPotsCollection.Setup(m => m.Find(p => p.Id == "1", null, default)).ReturnsAsync(existingPot);
//
//             // Act
//             var result = await _potLogic.UpdatePot("1", potUpdatedDto);
//
//             // Assert
//             Assert.AreEqual("Success", result);
//             _mockPotsCollection.Verify(
//                 m => m.ReplaceOneAsync(It.IsAny<FilterDefinition<Pot>>(), It.IsAny<Pot>(), null, default), Times.Once);
//         }
//
//         [Test]
//         public async Task DeletePot_Removes_Existing_Pot_And_Returns_Success_Message()
//         {
//             // Arrange
//
//             // Act
//             var result = await _potLogic.DeletePot("1");
//
//             // Assert
//             Assert.AreEqual("Success", result);
//             _mockPotsCollection.Verify(m => m.DeleteOneAsync(It.IsAny<FilterDefinition<Pot>>(), null, default),
//                 Times.Once);
//         }
//     }
// }