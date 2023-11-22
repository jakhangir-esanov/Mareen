using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.HotelAttachments;
using Mareen.Service.Exceptions;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mareen.Service.Services;
public class HotelAttachmentService : IHotelAttachmentService
{
    private readonly IRepository<HotelAttachment> repository;
    private readonly IMapper mapper;
    public HotelAttachmentService(IRepository<HotelAttachment> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<HotelAttachmentResultDto> AddAsync(HotelAttachmentCreationDto dto)
    {
        var hotelAttachment = await repository.SelectNoFilterAsync(x => x.AttachmentId.Equals(dto.AttachmentId));
        if (hotelAttachment is not null)
            throw new AlreadyExistException("Already exist!");

        var mapHotelAttachment = mapper.Map<HotelAttachment>(dto);
        await repository.InsertAsync(mapHotelAttachment);
        await repository.SaveAsync();

        var res = mapper.Map<HotelAttachmentResultDto>(mapHotelAttachment);
        return res;
    }

    public async Task<HotelAttachmentResultDto> ModifyAsync(HotelAttachmentUpdateDto dto)
    {
        var hotelAttachment = await repository.SelectNoFilterAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapHotelAttachment = mapper.Map(dto, hotelAttachment);
        repository.Update(mapHotelAttachment);
        await repository.SaveAsync();

        var res = mapper.Map<HotelAttachmentResultDto>(mapHotelAttachment);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var hotelAttachment = await repository.SelectNoFilterAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Drop(hotelAttachment);
        await repository.SaveAsync();

        return true;
    }

    public async Task<HotelAttachmentResultDto> RetrieveByIdAsync(long id)
    {
        var hotelAttachment = await repository.SelectNoFilterAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<HotelAttachmentResultDto>(hotelAttachment);
        return res;
    }

    public async Task<IEnumerable<HotelAttachmentResultDto>> RetrieveAllAsync()
    {
        var hotelAttachments = await repository.SelectAll().ToListAsync();
        var res = mapper.Map<IEnumerable<HotelAttachmentResultDto>>(hotelAttachments);
        return res;
    }
}
