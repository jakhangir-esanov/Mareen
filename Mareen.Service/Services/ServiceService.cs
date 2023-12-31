﻿using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.Attachments;
using Mareen.Service.DTOs.ServiceAttachments;
using Mareen.Service.DTOs.Services;
using Mareen.Service.Exceptions;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mareen.Service.Services;

public class ServiceService : IServiceService
{
    private readonly IRepository<Domain.Entities.Service> repository;
    private readonly IRepository<ServiceAttachment> serviceAttachmentRepository;
    private readonly IServiceAttachmentService serviceAttachmentService;
    private readonly IAttachmentService attachmentService;
    private readonly IMapper mapper;
    public ServiceService(IRepository<Domain.Entities.Service> repository, IMapper mapper,
        IServiceAttachmentService serviceAttachmentService, IAttachmentService attachmentService,
        IRepository<ServiceAttachment> serviceAttachmentRepository)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.attachmentService = attachmentService;
        this.serviceAttachmentService = serviceAttachmentService;
        this.serviceAttachmentRepository = serviceAttachmentRepository;
    }

    public async Task<ServiceResultDto> AddAsync(ServiceCreationDto dto)
    {
        var service = await repository.SelectAsync(x => x.ServiceName.Equals(dto.ServiceName));
        if (service is not null)
            throw new AlreadyExistException("Already exist!");

        service = await repository.SelectNoFilterAsync(x => x.ServiceName.Equals(dto.ServiceName));
        if (service is not null)
        {
            mapper.Map(dto, service);
            repository.Update(service);
        }
        else
        {
            service = mapper.Map<Domain.Entities.Service>(dto);
            await repository.InsertAsync(service);
        }
        await repository.SaveAsync();

        var res = mapper.Map<ServiceResultDto>(service);
        return res;
    }

    public async Task<ServiceResultDto> ModifyAsync(ServiceUpdateDto dto)
    {
        var service = await repository.SelectAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapService = mapper.Map(dto, service);
        repository.Update(mapService);
        await repository.SaveAsync();

        var res = mapper.Map<ServiceResultDto>(mapService);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var service = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Delete(service);
        await repository.SaveAsync();

        return true;
    }

    public async Task<ServiceResultDto> RetrieveByIdAsync(long id)
    {
        var service = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<ServiceResultDto>(service);
        return res;
    }

    public async Task<IEnumerable<ServiceResultDto>> RetrieveAllAsync()
    {
        var service = await repository.SelectAll().ToListAsync();
        var res = mapper.Map<IEnumerable<ServiceResultDto>>(service);
        return res;
    }

    public async Task<ServiceResultDto> ImageUploadAsync(long serviceId, AttachmentCreationDto dto)
    {
        var service = await repository.SelectAsync(x => x.Id.Equals(serviceId))
            ?? throw new NotFoundException("Could not upload image, because service was not found!");

        var serviceAttachments = await serviceAttachmentRepository.SelectAll().ToListAsync();
        if (serviceAttachments.Count() == 10)
            throw new CustomException(429, "Out of limit image!");

        var createdAttachment = await attachmentService.UploadAsync("ServiceFile", dto);

        var newServiceAttachment = new ServiceAttachmentCreationDto()
        {
            AttachmentId = createdAttachment.Id,
            ServiceId = serviceId
        };
        await serviceAttachmentService.AddAsync(newServiceAttachment);

        createdAttachment.ServiceAttachments = serviceAttachments;
        service.ServiceAttachments = serviceAttachments;

        return mapper.Map<ServiceResultDto>(service);
    }

    public async Task<ServiceResultDto> ModifyImageAsync(long serviceId, long attachmentId, AttachmentCreationDto dto)
    {
        var service = await repository.SelectAsync(x => x.Id.Equals(serviceId))
            ?? throw new NotFoundException("Could not update image, because service was not found!");

        var serviceAttachment = await serviceAttachmentRepository.SelectAsync(x => x.ServiceId.Equals(serviceId)
            && x.AttachmentId.Equals(attachmentId))
            ?? throw new NotFoundException("Attachment was not found!");

        await attachmentService.ModifyAsync("ServiceFile", attachmentId, dto);

        return mapper.Map<ServiceResultDto>(service);
    }
}
