using Microsoft.AspNetCore.Http;

namespace Mareen.Service.DTOs.Attachments;

public class AttachmentCreationDto
{
    public IFormFile FormFile { get; set; }
}
