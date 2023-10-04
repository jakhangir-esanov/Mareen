using Mareen.Domain.Entities;
using Mareen.Service.DTOs.Attachments;

namespace Mareen.Service.Interfaces;

public interface IAttachmentService
{
    Task<Attachment> UploadAsync(AttachmentCreationDto dto);
    Task<bool> RemoveAsync(Attachment attachment);
}
