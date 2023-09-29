using Mareen.Domain.Entities;
using Mareen.Domain.Enums;

namespace Mareen.Service.DTOs.Bookings;

public class BookingResultDto
{
    public long Id { get; set; }   
    public Room Room { get; set; }
    public Guest Guest { get; set; }
    public double Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Status Status { get; set; }
    public ICollection<BookingItem> BookingItems { get; set; }
}
