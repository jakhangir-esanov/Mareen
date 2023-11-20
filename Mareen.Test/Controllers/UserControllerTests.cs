using Mareen.Service.DTOs.Users;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.Test.Controllers;

public class UserControllerTests
{
    private readonly IUserService userService;
    private readonly UserController userController;
    public UserControllerTests()
    {
        this.userService = A.Fake<IUserService>();
        this.userController = new UserController(this.userService);
    }

    [Fact]
    public async void ShouldCreateUserSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var user = new UserCreationDto()
        {
            DateOfBirth = DateTime.UtcNow,
            Email = "example@gmail.com",
            FirstName = "Test",
            LastName = "Test",
            HotelId = 1,
            Password = "password",
            PhoneNumber = "1234567890",
            UserRole = Domain.Enums.Role.admin
        };

        //Act
        var result = await userController.CreateAsync(user);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldUpdateUserSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        var user = new UserUpdateDto()
        {
            DateOfBirth = DateTime.UtcNow,
            Email = "update@gmail.com",
            FirstName = "UpdatedFirstName",
            LastName = "UpdatedLastName",
            HotelId = 2,
            Password = "password",
            PhoneNumber = "1234567890",
            UserRole = Domain.Enums.Role.manager
        };

        //Act
        var result = await userController.UpdateAsync(user);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldDeleteUserSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await userController.DeleteAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetByIdUserSuccessfullyReturnStatusCodeOk()
    {
        //Arrange
        long id = 1;

        //Act
        var result = await userController.GetByIdAsync(id);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGetAllUsersSuccessfullyReturnStatusCodeOk()
    {
        //Arrange

        //Act
        var result = await userController.GetAllAsync();

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
