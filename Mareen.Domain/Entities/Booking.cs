using Mareen.Domain.Commons;
using Mareen.Domain.Enums;

namespace Mareen.Domain.Entities;

public sealed class Booking : Auditable
{
    public long RoomId { get; set; }
    public Room Room { get; set; }
    public long GuestId { get; set; }
    public Guest Guest { get; set; }
    public double Amount { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Status Status { get; set; }
    public ICollection<BookingItem> BookingItems { get; set; }
}
