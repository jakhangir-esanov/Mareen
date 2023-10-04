namespace Mareen.Service.Interfaces;

public interface IAuthService
{
    Task<string> GenerateTokenForUserAsync(string email, string password);
    Task<string> GenerateTokenForGuestAsync(string email, string password);
}
