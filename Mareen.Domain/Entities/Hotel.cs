using Mareen.Domain.Commons;
using Mareen.Domain.Enums;

namespace Mareen.Domain.Entities;

public sealed class Hotel : Auditable
{
    public string HotelName { get; set; }
    public Rating Rating { get; set; }
    public string Address { get; set; }
    public string City { get; set; }    
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Website { get; set; }
    public int Capacity { get; set; }
    public string Description { get; set; }
    public ICollection<Room> Rooms { get; set; }
    public ICollection<User> Employees { get;}

    public ICollection<HotelAttachment> HotelAttachments { get; set; }
    public ICollection<Attachment> Attachments { get; set; }
}
