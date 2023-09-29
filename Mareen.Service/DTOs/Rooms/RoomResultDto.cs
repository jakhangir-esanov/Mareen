using Mareen.Domain.Entities;
using Mareen.Domain.Enums;

namespace Mareen.Service.DTOs.Rooms;

public class RoomResultDto
{
    public long Id { get; set; }
    public Hotel Hotel { get; set; }
    public int RoomNumber { get; set; }
    public RoomType RoomType { get; set; }
    public double Price { get; set; }
    public string Description { get; set; }
    public bool IsFree { get; set; }
}