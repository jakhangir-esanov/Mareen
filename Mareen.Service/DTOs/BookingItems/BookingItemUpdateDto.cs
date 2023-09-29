namespace Mareen.Service.DTOs.BookingItems;

public class BookingItemUpdateDto
{
    public long Id { get; set; }
    public long ServiceId { get; set; }
    public int Quantity { get; set; }
    public long BookingId { get; set; }
}

