using Mareen.Domain.Commons;
using Mareen.Domain.Enums;

namespace Mareen.Domain.Entities;

public sealed class Room : Auditable
{
    public long HotelId { get; set; }
    public Hotel Hotel { get; set; }

    public int RoomNumber { get; set; }
    public RoomType RoomType { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public bool IsFree { get; set; }

    public long AttachmentId { get; set; }
    public Attachment Attachment { get; set; }
}
