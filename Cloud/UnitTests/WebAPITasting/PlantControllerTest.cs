

using WebAPI.Controllers.ControllerFrontEnd;

namespace UnitTests.WebAPITesting
{
    public class PlantControllerTest
    {
        private PlantController _plantController;
        private Mock<IPlantLogic> _plantLogic;

        [SetUp]
        public void Setup()
        {
            _plantLogic = new Mock<IPlantLogic>();
            _plantController = new PlantController(_plantLogic.Object);
        }

        [Test]
        public async Task Get_All_Plants()
        {
            // Arrange
            var plants = new List<Plant>
            {
                new Plant { NameOfPlant = "Plant1" },
                new Plant { NameOfPlant = "Plant2" }
            };

            var plantGetAllDto = new PlantGetAllDto
            {
                Plants = plants,
                Message = "All plants found.",
                Success = true
            };

            _plantLogic.Setup(pl => pl.GetAllPlants()).ReturnsAsync(plantGetAllDto);

            // Act
            var result = await _plantController.Get();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedDto = okResult.Value as PlantGetAllDto;
            Assert.IsNotNull(returnedDto);
            Assert.AreEqual(plantGetAllDto, returnedDto);
        }

        [Test]
        public async Task Get_Plant_By_Name()
        {
            // Arrange
            var plant = new Plant { NameOfPlant = "Plant1" };
            var plantGetByNameDto = new PlantGetByNameDto("Plant1")
            {
                Plant = plant,
                Message = "Plant with name Plant1 found.",
                Success = true
            };

            _plantLogic.Setup(pl => pl.GetPlantByName(It.IsAny<PlantGetByNameDto>())).ReturnsAsync(plantGetByNameDto);

            // Act
            var result = await _plantController.Get("Plant1");

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedDto = okResult.Value as PlantGetByNameDto;
            Assert.IsNotNull(returnedDto);
            Assert.AreEqual(plantGetByNameDto, returnedDto);
        }

        [Test]
        public async Task Create_Plant()
        {
            // Arrange
            var plant = new Plant
            {
                NameOfPlant = "NewPlant",
                SoilMinimumMoisture = 30,
                AmountOfWaterToBeGiven = 500,
                Image = "image.jpg",
                UserId = "user1"
            };

            var plantCreationDto = new PlantCreationDto(plant)
            {
                Message = "Plant created successfully.",
                Success = true
            };

            _plantLogic.Setup(pl => pl.CreatePlant(It.IsAny<PlantCreationDto>())).ReturnsAsync(plantCreationDto);

            var createPlantRequestDto = new CreatePlantRequestDto
            {
                NameOfPlant = "NewPlant",
                SoilMinimumMoisture = 30,
                AmountOfWaterToBeGiven = 500,
                Image = "image.jpg",
                UserId = "user1"
            };

            // Act
            var result = await _plantController.Post(createPlantRequestDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedDto = okResult.Value as PlantCreationDto;
            Assert.IsNotNull(returnedDto);
            Assert.AreEqual(plantCreationDto, returnedDto);
        }

        [Test]
        public async Task Update_Plant()
        {
            // Arrange
            var plant = new Plant
            {
                Id = "123",
                NameOfPlant = "UpdatedPlant",
                SoilMinimumMoisture = 35,
                AmountOfWaterToBeGiven = 600,
                Image = "updated_image.jpg"
            };

            var plantUpdateDto = new PlantUpdateDto("OldPlantName", plant)
            {
                Message = "Plant updated successfully.",
                Success = true
            };

            _plantLogic.Setup(pl => pl.UpdatePlant(It.IsAny<PlantUpdateDto>())).ReturnsAsync(plantUpdateDto);

            var updatePlantRequestDto = new UpdatePlantRequestDto
            {
                Id = "123",
                NameOfPlant = "UpdatedPlant",
                SoilMinimumMoisture = 35,
                AmountOfWaterToBeGiven = 600,
                Image = "updated_image.jpg"
            };

            // Act
            var result = await _plantController.Put("OldPlantName", updatePlantRequestDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedDto = okResult.Value as PlantUpdateDto;
            Assert.IsNotNull(returnedDto);
            Assert.AreEqual(plantUpdateDto, returnedDto);
        }

        [Test]
        public async Task Delete_Plant()
        {
            // Arrange
            var plantDeleteDto = new PlantDeleteDto("PlantToDelete")
            {
                Message = "Plant deleted with name PlantToDelete successfully.",
                Success = true
            };

            _plantLogic.Setup(pl => pl.DeletePlant(It.IsAny<PlantDeleteDto>())).ReturnsAsync(plantDeleteDto);

            // Act
            var result = await _plantController.Delete("PlantToDelete");

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedDto = okResult.Value as PlantDeleteDto;
            Assert.IsNotNull(returnedDto);
            Assert.AreEqual(plantDeleteDto, returnedDto);
        }
    }
}
