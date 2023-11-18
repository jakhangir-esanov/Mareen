using Mareen.Service.Interfaces;
using Mareen.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mareen.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("user/login")]
    public async Task<IActionResult> GenerateTokenForUserAsync(string email, string password)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.authService.GenerateTokenForUserAsync(email, password)
        });

    [HttpPost("guest/login")]
    public async Task<IActionResult> GenerateTokenForGuestAsync(string email, string password)
        => Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await this.authService.GenerateTokenForGuestAsync(email, password)
        });
}
