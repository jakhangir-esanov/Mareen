using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.Attachments;
using Mareen.Service.DTOs.Users;
using Mareen.Service.Exceptions;
using Mareen.Service.Helpers;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mareen.Service.Services;

public class UserService : IUserService
{
    private readonly IAttachmentService attachmentService;
    private readonly IRepository<User> repository;
    private readonly IMapper mapper;
    public UserService(IRepository<User> repository, IMapper mapper, IAttachmentService attachmentService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.attachmentService = attachmentService;
    }

    public async Task<UserResultDto> AddAsync(UserCreationDto dto)
    {
        var user = await repository.SelectAsync(x => x.Email.Equals(dto.Email));
        if (user is not null)
            throw new AlreadyExistException("Already exist!");

        user = await repository.SelectNoFilterAsync(x => x.Email.Equals(dto.Email));
        if (user is not null)
        {
            mapper.Map(dto, user);
            repository.Update(user);
        }
        else
        {
            var hashResult = PasswordHasher.Hash(dto.Password);

            user = mapper.Map<User>(dto);
            user.Password = hashResult.Password;
            user.Salt = hashResult.Salt;
            await repository.InsertAsync(user);
        }
        await repository.SaveAsync();

        var res = mapper.Map<UserResultDto>(user);
        return res;
    }

    public async Task<UserResultDto> ModifyAsync(UserUpdateDto dto)
    {
        var user = await repository.SelectAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapUser = mapper.Map(dto, user);
        repository.Update(mapUser);
        await repository.SaveAsync();

        var res = mapper.Map<UserResultDto>(mapUser);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var user = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Delete(user);
        await repository.SaveAsync();

        return true;
    }

    public async Task<UserResultDto> RetrieveByIdAsync(long id)
    {
        var user = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<UserResultDto>(user);
        return res;
    }

    public async Task<IEnumerable<UserResultDto>> RetrieveAllAsync()
    {
        var user = await repository.SelectAll().ToListAsync();
        var res = mapper.Map<IEnumerable<UserResultDto>>(user);
        return res;
    }

    public async Task<UserResultDto> ImageUploadAsync(long userId, AttachmentCreationDto dto)
    {
        var user = await this.repository.SelectAsync(x => x.Id.Equals(userId))
            ?? throw new NotFoundException("Not found!");

        var createAttachment = await this.attachmentService.UploadAsync(dto);
        user.AttachmentId = createAttachment.Id;
        user.Attachment = createAttachment;

        this.repository.Update(user);
        await this.repository.SaveAsync();

        return mapper.Map<UserResultDto>(user);
    }

    public async Task<UserResultDto> ModifyImageAsync(long userId, AttachmentCreationDto dto)
    {
        var user = await this.repository.SelectAsync(x => x.Id.Equals(userId))
            ?? throw new NotFoundException("Not found!");

        await this.attachmentService.RemoveAsync(user.Attachment);

        var createAttachment = await this.attachmentService.UploadAsync(dto);
        user.AttachmentId = createAttachment.Id;
        user.Attachment = createAttachment;

        this.repository.Update(user);
        await this.repository.SaveAsync();

        return mapper.Map<UserResultDto>(user);
    }
}
