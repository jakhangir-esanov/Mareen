using Mareen.Service.DTOs.Hotels;
using Mareen.Service.DTOs.Rooms;
using Mareen.Service.DTOs.Users;

namespace Mareen.Service.Interfaces;

public interface IHotelService
{
    Task<HotelResultDto> AddAsync(HotelCreationDto dto);
    Task<HotelResultDto> ModifyAsync(HotelUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<HotelResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<HotelResultDto>> RetrieveAllAsync();
    Task<IEnumerable<RoomResultDto>> RetrieveAllRoomsAsync(long hotelId);
    Task<IEnumerable<UserResultDto>> RetrieveAllEmployeesAsync(long hotelId);
}
