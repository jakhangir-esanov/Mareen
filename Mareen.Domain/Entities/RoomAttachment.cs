using Mareen.Domain.Commons;

namespace Mareen.Domain.Entities;

public sealed class RoomAttachment 
{
    public long Id { get; set; }
    public long RoomId { get; set; }
    public long AttachmentId { get; set; }

    public Room Room { get; set; }
    public Attachment Attachment { get; set; }
}
