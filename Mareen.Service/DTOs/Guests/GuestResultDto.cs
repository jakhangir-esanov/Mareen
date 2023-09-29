﻿using Mareen.Domain.Entities;

namespace Mareen.Service.DTOs.Guests;

public class GuestResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PassportDetails { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<PaymentHistory> Transactions { get; set; }
    public ICollection<Booking> Bookings { get; }
}



