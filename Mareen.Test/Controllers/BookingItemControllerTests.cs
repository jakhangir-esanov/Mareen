using Mareen.Service.DTOs.BookingItems;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.Test.Controllers;

public class BookingItemControllerTests
{
    private readonly IBookingItemService bookingItemService;
    private readonly BookingItemController bookingItemController;
    public BookingItemControllerTests()
    {
        this.bookingItemService = A.Fake<IBookingItemService>();
        this.bookingItemController = new BookingItemController(this.bookingItemService);
    }

    [Fact]
    public async void ShouldCreateBookingItemSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var bookingItem = new BookingItemCreationDto()
        {
            BookingId = 1,
            Quantity = 1,
            ServiceId = 1
        };

        //Act
        var result = await bookingItemController.CreateAsync(bookingItem);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldUpdateBookingItemSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var bookingItem = new BookingItemUpdateDto()
        {
            BookingId = 1,
            Quantity = 1,
            ServiceId = 1
        };

        //Act
        var result = await bookingItemController.UpdateAsync(bookingItem);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldDeleteBookingItemSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await bookingItemController.DeleteAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetByIdBookingItemSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1; 

        //Act
        var result = await bookingItemController.GetByIdAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllBookingItemsSuccessfullyReturnStatusCodeOk()
    {
        //Arrange

        //Act
        var result = await bookingItemController.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
