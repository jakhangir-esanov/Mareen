using Grpc.Core;
using Mareen.Service.DTOs.Services;
using Mareen.Service.Interfaces;

namespace Mareen.gRPC.Services;

public class ServiceService : service.serviceBase
{
    private readonly IServiceService serviceService;

    public ServiceService(IServiceService serviceService)
    {
        this.serviceService = serviceService;
    }

    public override async Task<ServiceCreationResponse> CreateAsync(ServiceCreationRequest request, ServerCallContext context)
    {
        var service = new ServiceCreationDto()
        {
            Description = request.Description,
            Price = request.Price,
            ServiceName = request.ServiceName
        };

        var result = await serviceService.AddAsync(service);

        return await Task.FromResult(new ServiceCreationResponse
        {
            Id = result.Id,
            Description = result.Description,
            Price = result.Price,
            ServiceName = result.ServiceName
        });
    }

    public override async Task<ServiceUpdateResponse> UpdateAsync(ServiceUpdateRequest request, ServerCallContext context)
    {
        var service = new ServiceUpdateDto()
        {
            Id = request.Id,
            Description = request.Description,
            Price = request.Price,
            ServiceName = request.ServiceName
        };

        var result = await serviceService.ModifyAsync(service);

        return await Task.FromResult(new ServiceUpdateResponse
        {
            Id = result.Id,
            Description = result.Description,
            Price = result.Price,
            ServiceName = result.ServiceName
        });
    }

    public override async Task<ServiceDeleteResponse> DeleteAsync(ServiceDeleteRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await serviceService.RemoveAsync(id);

        return await Task.FromResult(new ServiceDeleteResponse
        {
            IsDelete = result
        });
    }

    public override async Task<ServiceGetResponse> GetByIdAsync(ServiceGetRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await serviceService.RetrieveByIdAsync(id);

        return await Task.FromResult(new ServiceGetResponse
        {
            Id = result.Id,
            Description = result.Description,
            Price = result.Price,
            ServiceName = result.ServiceName
        });
    }

    public override async Task<ServiceGetAllResponse> GetAllAsync(ServiceGetAllRequest request, ServerCallContext context)
    {
        var result = await serviceService.RetrieveAllAsync();

        var response = new ServiceGetAllResponse();

        foreach (var item in result)
        {
            var service = new ServiceGetResponse
            {
                Id = item.Id,
                Description = item.Description,
                Price = item.Price,
                ServiceName = item.ServiceName,
            };

            response.Sth.Add(service);
        }

        return response;
    }
}
