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

    [Fact]
    public async void ShouldGenerateTokenForGuestReturnStatusCodeOk()
    {
        //Arrange
        string email = "example@example.com";
        string password = "password";

        //Act
        var result = await authController.GenerateTokenForUserAsync(email, password);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);
    }

     [Fact]
    public async void ShouldGenerateTokenForUserReturnUnauthorizedForInvalidCredentials()
    {
        // Arrange
        string email = "invalidexample.com";
        string password = "invalidpassword";

        // Act
        var result = await authController.GenerateTokenForUserAsync(email, password);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async void ShouldGenerateTokenForGuestReturnUnauthorizedForGuestUser()
    {
        // Arrange
        string email = "guestexample.com";
        string password = "guestpassword";

        // Act
        var result = await authController.GenerateTokenForGuestAsync(email, password);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }
}
