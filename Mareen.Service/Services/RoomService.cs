using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.Attachments;
using Mareen.Service.DTOs.Rooms;
using Mareen.Service.Exceptions;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mareen.Service.Services;

public class RoomService : IRoomService
{
    private readonly IAttachmentService attachmentService;
    private readonly IRepository<Room> repository;
    private readonly IRepository<Hotel> repository1;
    private readonly IMapper mapper;
    public RoomService(IRepository<Room> repository, IMapper mapper, IRepository<Hotel> repository1, IAttachmentService attachmentService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.repository1 = repository1;
        this.attachmentService = attachmentService;
    }

    public async Task<RoomResultDto> AddAsync(RoomCreationDto dto)
    {
        var rooms = await repository.SelectAll().ToListAsync();
        var hotel = await repository1.SelectAsync(x => x.Id.Equals(dto.HotelId));
        if (hotel.Capacity == rooms.Count())
            throw new AlreadyExistException("Out of capacity. You couldn't add room!");

        var room = await repository.SelectAsync(x => x.RoomNumber.Equals(dto.RoomNumber));
        if (room is not null)
            throw new AlreadyExistException("Already exist!");

        room = await repository.SelectNoFilterAsync(x => x.RoomNumber.Equals(dto.RoomNumber));
        if (room is not null)
        {
            mapper.Map(dto, room);
            repository.Update(room);
        }
        else
        {
            room = mapper.Map<Room>(dto);
            room.IsFree = true;
            await repository.InsertAsync(room);
        }
        await repository.SaveAsync();

        var res = mapper.Map<RoomResultDto>(room);
        return res;
    }

    public async Task<RoomResultDto> ModifyAsync(RoomUpdateDto dto)
    {
        var room = await repository.SelectAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapRoom = mapper.Map(dto, room);
        repository.Update(mapRoom);
        await repository.SaveAsync();

        var res = mapper.Map<RoomResultDto>(mapRoom);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var room = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Delete(room);
        await repository.SaveAsync();

        return true;
    }

    public async Task<RoomResultDto> RetrieveByIdAsync(long id)
    {
        var room = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<RoomResultDto>(room);
        return res;
    }

    public async Task<IEnumerable<RoomResultDto>> RetrieveAllAsync()
    {
        var room = await repository.SelectAll().ToListAsync();
        var res = mapper.Map<IEnumerable<RoomResultDto>>(room);
        return res;
    }

    public async Task<IEnumerable<RoomResultDto>> RetrieveAllAvailableAsync()
    {
        var room = await repository.SelectAll()
            .Where(x => x.IsFree == true)
            .ToListAsync();

        var res = mapper.Map<IEnumerable<RoomResultDto>>(room);
        return res;
    }

    public async Task<RoomResultDto> ImageUploadAsync(long roomId, AttachmentCreationDto dto)
    {
        var room = await this.repository.SelectAsync(x => x.Id.Equals(roomId))
            ?? throw new NotFoundException("Not found!");

        var createAttachment = await this.attachmentService.UploadAsync(dto);
        room.AttachmentId = createAttachment.Id;
        room.Attachment = createAttachment;

        this.repository.Update(room);
        await this.repository.SaveAsync();

        return mapper.Map<RoomResultDto>(room);
    }

    public async Task<RoomResultDto> ModifyImageAsync(long roomId, AttachmentCreationDto dto)
    {
        var room = await this.repository.SelectAsync(x => x.Id.Equals(roomId))
            ?? throw new NotFoundException("Not found!");

        await this.attachmentService.RemoveAsync(room.Attachment);

        var createAttachment = await this.attachmentService.UploadAsync(dto);
        room.AttachmentId = createAttachment.Id;
        room.Attachment = createAttachment;

        this.repository.Update(room);
        await this.repository.SaveAsync();

        return mapper.Map<RoomResultDto>(room);
    }
}
