using Mareen.Service.DTOs.Attachments;
using Mareen.Service.DTOs.Users;

namespace Mareen.Service.Interfaces;

public interface IUserService
{
    Task<UserResultDto> AddAsync(UserCreationDto dto);
    Task<UserResultDto> ModifyAsync(UserUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<UserResultDto> ImageUploadAsync(long userId, AttachmentCreationDto dto);
    Task<UserResultDto> ModifyImageAsync(long userId, AttachmentCreationDto dto);
    Task<UserResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<UserResultDto>> RetrieveAllAsync();
}
