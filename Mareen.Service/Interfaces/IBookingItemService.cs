using Mareen.Service.DTOs.BookingItems;

namespace Mareen.Service.Interfaces;

public interface IBookingItemService
{
    Task<BookingItemResultDto> AddAsync(BookingItemCreationDto dto);
    Task<BookingItemResultDto> ModifyAsync(BookingItemUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<BookingItemResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<BookingItemResultDto>> RetrieveAllAsync();
}
