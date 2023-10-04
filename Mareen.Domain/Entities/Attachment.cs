using Mareen.Domain.Commons;

namespace Mareen.Domain.Entities;

public class Attachment : Auditable
{
    public string FilePath { get; set; }
    public string FileName { get; set; }
}
