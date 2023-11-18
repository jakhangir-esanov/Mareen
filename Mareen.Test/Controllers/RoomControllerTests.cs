using Mareen.Service.DTOs.Rooms;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.Test.Controllers;

public class RoomControllerTests
{
    private readonly IRoomService roomService;
    private readonly RoomController roomController;
    public RoomControllerTests()
    {
        this.roomService = A.Fake<IRoomService>();
        this.roomController = new RoomController(this.roomService);
    }

    [Fact]
    public async void ShouldCreateRoomSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var room = new RoomCreationDto()
        {
            Description = "description",
            HotelId = 1,
            IsFree = true,
            Price = 1,
            RoomNumber = 1,
            RoomType = Domain.Enums.RoomType.economy
        };

        //Act
        var result = await roomController.CreateAsync(room);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldUpdateRoomSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var room = new RoomUpdateDto()
        {
            Id = 1,
            Description = "description",
            HotelId = 2,
            IsFree = false,
            Price = 2,
            RoomNumber = 2,
            RoomType = Domain.Enums.RoomType.luxary
        };

        //Act
        var result = await roomController.UpdateAsync(room);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldDeleteRoomSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act 
        var result = await roomController.DeleteAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetByIdRoomSuccessfullyReturnStatusCodeOk()
    {
        //Arrange 
        long id = 1;

        //Act
        var result = await roomController.GetByIdAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllRoomsSuccessfullyReturnStatusCodeOk()
    {
        //Arrange

        //Act
        var result = await roomController.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllFreeRoomsSuccessfullyReturnStatusCodeOk()
    {
        //Arrange

        //Act
        var result = await roomController.GetAllAvailableRoomsAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
