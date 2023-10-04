using Mareen.Service.DTOs.Attachments;
using Mareen.Service.DTOs.Bookings;
using Mareen.Service.DTOs.Guests;
using Mareen.Service.DTOs.PaymentHistories;

namespace Mareen.Service.Interfaces;

public interface IGuestService
{
    Task<GuestResultDto> AddAsync(GuestCreationDto dto);
    Task<GuestResultDto> ModifyAsync(GuestUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<GuestResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<GuestResultDto>> RetrieveAllAsync();
    Task<GuestResultDto> ImageUploadAsync(long guestId, AttachmentCreationDto dto);
    Task<GuestResultDto> ModifyImageAsync(long guestId, AttachmentCreationDto dto);
    Task<IEnumerable<PaymentHistoryResultDto>> RetrieveAllPaymentHistoriesAsync(long guestId);
    Task<IEnumerable<BookingResultDto>> RetrieveAllBookingsAsync(long guestId);
}
