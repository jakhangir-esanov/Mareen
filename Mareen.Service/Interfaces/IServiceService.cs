using Mareen.Service.DTOs.Attachments;
using Mareen.Service.DTOs.Services;

namespace Mareen.Service.Interfaces;

public interface IServiceService
{
    Task<ServiceResultDto> AddAsync(ServiceCreationDto dto);
    Task<ServiceResultDto> ModifyAsync(ServiceUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<ServiceResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<ServiceResultDto>> RetrieveAllAsync();
    Task<ServiceResultDto> ImageUploadAsync(long serviceId, AttachmentCreationDto dto);
    Task<ServiceResultDto> ModifyImageAsync(long serviceId, long attachmentId, AttachmentCreationDto dto);
}
