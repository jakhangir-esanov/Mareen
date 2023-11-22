using Mareen.Domain.Commons;

namespace Mareen.Domain.Entities;

public sealed class HotelAttachment : Auditable
{
    public long HotelId { get; set; }
    public long AttachmentId { get; set; }

    public Hotel Hotel { get; set; }
    public Attachment Attachment { get; set; }
}
