using Mareen.Service.DTOs.Hotels;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.Test.Controllers;

public class HotelControllerTests
{
    private readonly IHotelService hotelService;
    private readonly HotelController hotelController;
    public HotelControllerTests()
    {
        this.hotelService = A.Fake<IHotelService>();
        this.hotelController = new HotelController(this.hotelService);
    }

    [Fact]
    public async void ShouldCreateHotelSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var hotel = new HotelCreationDto()
        {
            Address = "Muqumiy",
            Capacity = 100,
            City = "Tashkent",
            Country = "Uzbekistan",
            Description = "Description",
            Email = "hotel@example.com",
            HotelName = "GoldPlaza",
            PhoneNumber = "1234567890",
            PostalCode = "123456",
            State = "Chilonzor",
            Rating = Domain.Enums.Rating.good,
            Website = "hotel.com"
        };

        //Act
        var result = await hotelController.CreateAsync(hotel);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldUpdateHotelSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var hotel = new HotelUpdateDto()
        {
            Address = "Updated",
            Capacity = 200,
            City = "Surxondaryo",
            Country = "Uzbekistan",
            Description = "Description",
            Email = "hotel@example.com",
            HotelName = "OldPlaza",
            PhoneNumber = "1234567890",
            PostalCode = "331434",
            State = "Chilonzor",
            Rating = Domain.Enums.Rating.excellent,
            Website = "hotelhotel.com"
        };

        //Act
        var result = await hotelController.UpdateAsync(hotel);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldDeleteHotelSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await hotelController.DeleteAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetByIdSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await hotelController.GetByIdAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllHotelsSuccessfullyReturnStatusCodeOk()
    {
        //Arrange

        //Act
        var result = await hotelController.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllEmployeesSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await hotelController.GetAllEmployeesAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllRoomsSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await hotelController.GetAllRoomsAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
