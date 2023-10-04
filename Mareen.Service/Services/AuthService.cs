using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.Exceptions;
using Mareen.Service.Helpers;
using Mareen.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mareen.Service.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Guest> guestRepository;
    private readonly IConfiguration configuration;
    public AuthService(IRepository<User> userRepository, IRepository<Guest> guestRepository, IConfiguration configuration)
    {
        this.userRepository = userRepository;
        this.guestRepository = guestRepository;
        this.configuration = configuration;
    }

    public async Task<string> GenerateTokenForUserAsync(string email, string password)
    {
        var user = await userRepository.SelectAsync(x => x.Email.Equals(email))
            ?? throw new NotFoundException("User not found!");

        bool varifiedPassword = PasswordHasher.Verify(password, user.Password, user.Salt);
        if (!varifiedPassword)
            throw new CustomException(400, "Password or Email is incorrect!");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Email", user.Email),
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        string result = tokenHandler.WriteToken(token);
        return result;
    }
    public async Task<string> GenerateTokenForGuestAsync(string email, string password)
    {
        var guest = await guestRepository.SelectAsync(x => x.Email.Equals(email))
            ?? throw new NotFoundException("Guest not found!");

        bool varifiedPassword = PasswordHasher.Verify(password, guest.Password, guest.Salt);
        if (!varifiedPassword)
            throw new CustomException(400, "Password or Email is incorrect!");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Email", guest.Email),
                new Claim("Id", guest.Id.ToString()),
            }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        string result = tokenHandler.WriteToken(token);
        return result;
    }
}
