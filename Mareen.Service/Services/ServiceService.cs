using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Service.DTOs.Services;
using Mareen.Service.Exceptions;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mareen.Service.Services;

public class ServiceService : IServiceService
{
    private readonly IRepository<Domain.Entities.Service> repository;
    private readonly IMapper mapper;
    public ServiceService(IRepository<Domain.Entities.Service> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
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
        await repository.SelectAsync();

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
}
