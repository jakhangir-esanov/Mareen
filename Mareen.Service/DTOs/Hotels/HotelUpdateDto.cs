using Mareen.Domain.Enums;

namespace Mareen.Service.DTOs.Hotels;

public class HotelUpdateDto
{
    public long Id { get; set; }
    public string HotelName { get; set; }
    public Rating Rating { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
}
