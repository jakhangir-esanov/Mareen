using Mareen.Domain.Enums;

namespace Mareen.Service.DTOs.Rooms;

public class RoomUpdateDto
{
    public long Id { get; set; }
    public long HotelId { get; set; }
    public int RoomNumber { get; set; }
    public RoomType RoomType { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public bool IsFree { get; set; }
}
