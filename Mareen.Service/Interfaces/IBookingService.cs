using Mareen.Service.DTOs.BookingItems;
using Mareen.Service.DTOs.Bookings;

namespace Mareen.Service.Interfaces;

public interface IBookingService
{
    Task<BookingResultDto> AddAsync(BookingCreationDto dto);
    Task<BookingResultDto> ModifyAsync(BookingUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<BookingResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<BookingResultDto>> RetrieveAllAsync();
    Task<IEnumerable<BookingItemResultDto>> RetrieveAllBookingItemsAsync(long bookingId);
}
