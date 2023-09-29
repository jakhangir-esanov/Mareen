using Mareen.Domain.Entities;

namespace Mareen.Service.DTOs.BookingItems;

public class BookingItemResultDto
{
    public long Id { get; set; }
    public Domain.Entities.Service Service { get; set; }
    public int Quantity { get; set; }
    public Booking Booking { get; set; }
}

