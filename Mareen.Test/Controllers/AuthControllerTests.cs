using FakeItEasy;
using Mareen.Service.Interfaces;
using Mareen.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.Test.Controllers;

public class AuthControllerTests
{
    private readonly IAuthService authService;
    private readonly AuthController authController;
    public AuthControllerTests()
    {
        this.authService = A.Fake<IAuthService>();
        this.authController = new AuthController(this.authService);
    }

    [Fact]
    public async void ShouldGenerateTokenForUserReturnStatusCodeOk()
    {
        //Arrange
        string email = "example@gmail.com";
        string password = "password";

        //Act
        var result = await authController.GenerateTokenForUserAsync(email, password);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }
}
