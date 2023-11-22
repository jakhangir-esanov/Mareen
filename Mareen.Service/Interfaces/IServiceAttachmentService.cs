using Mareen.Service.DTOs.ServiceAttachments;

namespace Mareen.Service.Interfaces;

public interface IServiceAttachmentService
{
    public Task<ServiceAttachmentResultDto> AddAsync(ServiceAttachmentCreationDto dto);
    public Task<ServiceAttachmentResultDto> ModifyAsync(ServiceAttachmentUpdateDto dto);
    public Task<bool> RemoveAsync(long id);
    public Task<ServiceAttachmentResultDto> RetrieveByIdAsync(long id);
    public Task<IEnumerable<ServiceAttachmentResultDto>> RetrieveAllAsync();
}
