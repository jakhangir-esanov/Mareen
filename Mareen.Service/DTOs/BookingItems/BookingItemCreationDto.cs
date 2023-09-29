namespace Mareen.Service.DTOs.BookingItems;

public class BookingItemCreationDto
{
    public long ServiceId { get; set; }
    public int Quantity { get; set; }
    public long BookingId { get; set; }
}

