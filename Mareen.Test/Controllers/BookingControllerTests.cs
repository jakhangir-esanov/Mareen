using Mareen.Service.DTOs.Bookings;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.Test.Controllers;

public class BookingControllerTests
{
    private readonly IBookingService bookingService;
    private readonly BookingController bookingController;
    public BookingControllerTests()
    {
        this.bookingService = A.Fake<IBookingService>();
        this.bookingController = new BookingController(this.bookingService);
    }

    [Fact]
    public async void ShouldCreateBookingSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var booking = new BookingCreationDto()
        {
            Amount = 1,
            GuestId = 1,
            RoomId = 1,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow,
            Status = Domain.Enums.Status.inProgress
        };

        //Act
        var result = await bookingController.CreateAsync(booking);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldUpdateBookingSuccessfullyReturnStatusCodeOk()
    {
        //Arrange 
        var booking = new BookingUpdateDto()
        {
            Amount = 1,
            GuestId = 1,
            RoomId = 1,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow,
            Status = Domain.Enums.Status.confirmed
        };

        //Act
        var result = await bookingController.UpdateAsync(booking);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldDeleteBookingSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await bookingController.DeleteAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetByIdBookingSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await bookingController.GetByIdAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllBookingsSuccessfullyReturnStatusCodeOk()
    {
        //Arrange

        //Act
        var result = await bookingController.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result); 
    }
}
