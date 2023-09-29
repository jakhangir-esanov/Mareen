using Mareen.Domain.Entities;
using Mareen.Domain.Enums;

namespace Mareen.Service.DTOs.Users;

public class UserResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public Role UserRole { get; set; }
    public Hotel Hotel { get; set; }
}
