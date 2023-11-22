using Mareen.Service.DTOs.HotelAttachments;

namespace Mareen.Service.Interfaces;

public interface IHotelAttachmentService
{
    public Task<HotelAttachmentResultDto> AddAsync(HotelAttachmentCreationDto dto);
    public Task<HotelAttachmentResultDto> ModifyAsync(HotelAttachmentUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<HotelAttachmentResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<HotelAttachmentResultDto>> RetrieveAllAsync();
}
