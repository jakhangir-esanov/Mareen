using Mareen.Domain.Enums;

namespace Mareen.Service.DTOs.Bookings;

public class BookingUpdateDto
{
    public long Id { get; set; }
    public long RoomId { get; set; }
    public long GuestId { get; set; }
    public double Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Status Status { get; set; }
}
