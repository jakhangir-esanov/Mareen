using Mareen.Domain.Commons;

namespace Mareen.Domain.Entities;

public sealed class BookingItem : Auditable
{
    public long ServiceId { get; set; }
    public Service Service { get; set; }
    public int Quantity { get; set; }
    public long BookingId { get; set; }
    public Booking Booking { get; set; } 
}
