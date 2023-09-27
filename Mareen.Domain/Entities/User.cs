using Mareen.Domain.Commons;
using Mareen.Domain.Enums;

namespace Mareen.Domain.Entities;

public sealed class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public Role UserRole { get; set; }
}
