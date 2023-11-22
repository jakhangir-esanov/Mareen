using Mareen.Domain.Entities;
using Mareen.Service.DTOs.Attachments;

namespace Mareen.Service.Interfaces;

public interface IAttachmentService
{
    Task<Attachment> UploadAsync(string dirName, AttachmentCreationDto dto);
    Task<bool> RemoveAsync(long attachmentId);
}
