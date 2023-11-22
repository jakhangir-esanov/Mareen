using Mareen.Domain.Entities;

namespace Mareen.Service.DTOs.HotelAttachments;

public class HotelAttachmentResultDto
{
    public long Id { get; set; }
    public Hotel Hotel { get; set; }
    public Attachment Attachment { get; set; }
}