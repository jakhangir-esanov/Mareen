using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.RoomAttachments;
using Mareen.Service.Exceptions;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mareen.Service.Services;

public class RoomAttachmentService : IRoomAttachmentService
{
    private readonly IRepository<RoomAttachment> repository;
    private readonly IMapper mapper;
    public RoomAttachmentService(IRepository<RoomAttachment> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<RoomAttachmentResultDto> AddAsync(RoomAttachmentCreationDto dto)
    {
        var roomAttachment = await repository.SelectNoFilterAsync(x => x.AttachmentId.Equals(dto.AttachmentId));
        if (roomAttachment is not null)
            throw new AlreadyExistException("Already exist!");

        var mapRoomAttachment = mapper.Map<RoomAttachment>(dto);
        await repository.InsertAsync(mapRoomAttachment);
        await repository.SaveAsync();

        var res = mapper.Map<RoomAttachmentResultDto>(mapRoomAttachment);
        return res;
    }

    public async Task<RoomAttachmentResultDto> ModifyAsync(RoomAttachmentUpdateDto dto)
    {
        var roomAttachment = await repository.SelectNoFilterAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapRoomAttachment = mapper.Map(dto, roomAttachment);
        repository.Update(mapRoomAttachment);
        await repository.SaveAsync();

        var res = mapper.Map<RoomAttachmentResultDto>(mapRoomAttachment);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var roomAttachment = await repository.SelectNoFilterAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Drop(roomAttachment);
        await repository.SaveAsync();

        return true;
    }

    public async Task<RoomAttachmentResultDto> RetrieveByIdAsync(long id)
    {
        var roomAttachment = await repository.SelectNoFilterAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<RoomAttachmentResultDto>(roomAttachment);
        return res;
    }

    public async Task<IEnumerable<RoomAttachmentResultDto>> RetrieveAllAsync()
    {
        var roomAttachments = await repository.SelectAll().ToListAsync();
        var res = mapper.Map<IEnumerable<RoomAttachmentResultDto>>(roomAttachments);
        return res;
    }
}
