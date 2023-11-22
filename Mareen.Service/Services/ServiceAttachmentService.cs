using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.ServiceAttachments;
using Mareen.Service.Exceptions;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mareen.Service.Services;

public class ServiceAttachmentService : IServiceAttachmentService
{
    private readonly IRepository<ServiceAttachment> repository;
    private readonly IMapper mapper;
    public ServiceAttachmentService(IRepository<ServiceAttachment> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<ServiceAttachmentResultDto> AddAsync(ServiceAttachmentCreationDto dto)
    {
        var serviceAttachment = await repository.SelectNoFilterAsync(x => x.AttachmentId.Equals(dto.AttachmentId));
        if (serviceAttachment is not null)
            throw new AlreadyExistException("Already exist!");

        var mapServiceAttachment = mapper.Map<ServiceAttachment>(dto);
        await repository.InsertAsync(mapServiceAttachment);
        await repository.SaveAsync();

        var res = mapper.Map<ServiceAttachmentResultDto>(mapServiceAttachment);
        return res;
    }

    public async Task<ServiceAttachmentResultDto> ModifyAsync(ServiceAttachmentUpdateDto dto)
    {
        var serviceAttachment = await repository.SelectNoFilterAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapServiceAttachment = mapper.Map(dto, serviceAttachment);
        repository.Update(mapServiceAttachment);
        await repository.SaveAsync();

        var res = mapper.Map<ServiceAttachmentResultDto>(mapServiceAttachment);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var serviceAttachment = await repository.SelectNoFilterAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Drop(serviceAttachment);
        await repository.SaveAsync();

        return true;
    }

    public async Task<ServiceAttachmentResultDto> RetrieveByIdAsync(long id)
    {
        var serviceAttachment = await repository.SelectNoFilterAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<ServiceAttachmentResultDto>(serviceAttachment);
        return res;
    }

    public async Task<IEnumerable<ServiceAttachmentResultDto>> RetrieveAllAsync()
    {
        var serviceAttachments = await repository.SelectAll().ToListAsync();
        var res = mapper.Map<IEnumerable<ServiceAttachmentResultDto>>(serviceAttachments);
        return res;
    }
}
