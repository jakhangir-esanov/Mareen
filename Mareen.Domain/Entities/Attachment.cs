using Mareen.Domain.Commons;

namespace Mareen.Domain.Entities;

public class Attachment : Auditable
{
    public string FilePath { get; set; }
    public string FileName { get; set; }

    public ICollection<HotelAttachment> HotelAttachments { get; set; }
    public ICollection<RoomAttachment> RoomAttachments { get; set; }
    public ICollection<ServiceAttachment> ServiceAttachments { get; set; }
}
