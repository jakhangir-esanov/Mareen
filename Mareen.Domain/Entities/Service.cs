using Mareen.Domain.Commons;

namespace Mareen.Domain.Entities;

public sealed class Service : Auditable
{
    public string ServiceName { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }

    public ICollection<ServiceAttachment> ServiceAttachments { get; set; }
    public ICollection<Attachment> Attachments { get; set; }
}