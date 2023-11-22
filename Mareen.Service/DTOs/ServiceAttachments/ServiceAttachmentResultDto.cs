using Mareen.Domain.Entities;

namespace Mareen.Service.DTOs.ServiceAttachments;

public class ServiceAttachmentResultDto
{
    public long Id { get; set; }
    public Domain.Entities.Service Service { get; set; }
    public Attachment Attachment { get; set; }
}