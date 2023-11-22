using Mareen.Domain.Entities;

namespace Mareen.Service.DTOs.RoomAttachments;

public class RoomAttachmentResultDto
{
    public long Id { get; set; }
    public Room Room { get; set; }
    public Attachment Attachment { get; set; }
}
