using Mareen.Domain.Commons;

namespace Mareen.Domain.Entities;

public sealed class ServiceAttachment : Auditable
{
    public long ServiceId { get; set; }
    public long AttachmentId { get; set; }

    public Service Service { get; set; }
    public Attachment Attachment { get; set; }
}
