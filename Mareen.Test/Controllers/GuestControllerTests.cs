using Mareen.Service.DTOs.Guests;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.Test.Controllers;

public class GuestControllerTests
{
    private readonly IGuestService guestService;
    private readonly GuestController guestController;
    public GuestControllerTests()
    {
        this.guestService = A.Fake<IGuestService>();
        this.guestController = new GuestController(this.guestService);
    }

    [Fact]
    public async void ShouldCreateGuestSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var guest = new GuestCreationDto()
        {
            FirstName = "Test",
            LastName = "Test",
            Email = "example@gmail.com",
            Password = "Password",
            PhoneNumber = "1234567890",
            DateOfBirth = DateTime.UtcNow,
            PassportDetails = "test"
        };

        //Act
        var result = await guestController.CreateAsync(guest);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldUpdateGuestSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var guest = new GuestUpdateDto()
        {
            FirstName = "UpdatedFirstName",
            LastName = "UpdateLastName",
            Email = "updated@gmail.com",
            Password = "password",
            PhoneNumber = "1234567899",
            DateOfBirth = DateTime.UtcNow,
            PassportDetails = "testtest"
        };

        //Act
        var result = await guestController.UpdateAsync(guest);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldDeleteGuestSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await guestController.DeleteAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetByIdGuestSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await guestController.GetByIdAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllGuestsSuccessfullyReturnStatusCodeOk()
    {
        //Arrange

        //Act
        var result = await guestController.GetAllGuestAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllPaymentHistoriesSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await guestController.GetAllPaymentHisotriesAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllBookingsSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await guestController.GetAllBookingsAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}

