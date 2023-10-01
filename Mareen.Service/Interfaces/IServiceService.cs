using Mareen.Service.DTOs.Services;

namespace Mareen.Service.Interfaces;

public interface IServiceService
{
    Task<ServiceResultDto> AddAsync(ServiceCreationDto dto);
    Task<ServiceResultDto> ModifyAsync(ServiceUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<ServiceResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<ServiceResultDto>> RetrieveAllAsync();
}
