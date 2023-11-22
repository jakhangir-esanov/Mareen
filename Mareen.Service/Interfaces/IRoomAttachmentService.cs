using Mareen.Service.DTOs.RoomAttachments;

namespace Mareen.Service.Interfaces;

public interface IRoomAttachmentService
{
    public Task<RoomAttachmentResultDto> AddAsync(RoomAttachmentCreationDto dto);
    public Task<RoomAttachmentResultDto> ModifyAsync(RoomAttachmentUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<RoomAttachmentResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<RoomAttachmentResultDto>> RetrieveAllAsync();
}
