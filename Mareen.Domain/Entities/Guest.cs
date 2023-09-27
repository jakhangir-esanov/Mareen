﻿using Mareen.Domain.Commons;

namespace Mareen.Domain.Entities;

public sealed class Guest : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PassportDetails { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set;}
}