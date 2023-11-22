using Mareen.Domain.Commons;

namespace Mareen.Domain.Entities;

public sealed class RoomAttachment : Auditable
{
    public long RoomId { get; set; }
    public long AttachmentId { get; set; }

    public Room Room { get; set; }
    public Attachment Attachment { get; set; }
}
