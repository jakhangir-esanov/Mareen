using Mareen.Service.DTOs.Rooms;

namespace Mareen.Service.Interfaces;

public interface IRoomService
{
    Task<RoomResultDto> AddAsync(RoomCreationDto dto);
    Task<RoomResultDto> ModifyAsync(RoomUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<RoomResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<RoomResultDto>> RetrieveAllAsync();
    Task<IEnumerable<RoomResultDto>> RetrieveAllAvailableAsync();
}
