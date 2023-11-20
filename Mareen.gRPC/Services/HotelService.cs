using Grpc.Core;
using Mareen.Service.DTOs.Hotels;
using Mareen.Service.Interfaces;

namespace Mareen.gRPC.Services;

public class HotelService : hotel.hotelBase
{
    private readonly IHotelService hotelService;

    public HotelService(IHotelService hotelService)
    {
        this.hotelService = hotelService;
    }

    public override async Task<HotelCreationResponse> CreateAsync(HotelCreationRequest request, ServerCallContext context)
    {
        var hotel = new HotelCreationDto()
        {
            Address = request.Address,
            Capacity = request.Capacity,
            City = request.City,
            Country = request.Country,
            Description = request.Description,
            Email = request.Email,
            HotelName = request.HotelName,
            PhoneNumber = request.PhoneNumber,
            PostalCode = request.PostalCode,
            Rating = (Domain.Enums.Rating)request.Rating,
            State = request.State,
            Website = request.Website
        };

        var result = await hotelService.AddAsync(hotel);

        return await Task.FromResult(new HotelCreationResponse
        {
            Id = result.Id,
            Address = result.Address,
            Capacity = result.Capacity,
            City = result.City,
            Country = result.Country,
            Description = result.Description,
            Email = result.Email,
            HotelName = result.HotelName,
            PhoneNumber = result.PhoneNumber,
            PostalCode = result.PostalCode,
            Rating = (int)result.Rating,
            State = result.State,
            Website = result.Website
        });
    }

    public override async Task<HotelUpdateResponse> UpdateAsync(HotelUpdateRequest request, ServerCallContext context)
    {
        var hotel = new HotelUpdateDto
        {
            Id = request.Id,
            Address = request.Address,
            Capacity = request.Capacity,
            City = request.City,
            Country = request.Country,
            Description = request.Description,
            Email = request.Email,
            HotelName = request.HotelName,
            PhoneNumber = request.PhoneNumber,
            PostalCode = request.PostalCode,
            Rating = (Domain.Enums.Rating)request.Rating,
            State = request.State,
            Website = request.Website
        };

        var result = await hotelService.ModifyAsync(hotel);

        return await Task.FromResult(new HotelUpdateResponse
        {
            Id = result.Id,
            Address = result.Address,
            Capacity = result.Capacity,
            City = result.City,
            Country = result.Country,
            Description = result.Description,
            Email = result.Email,
            HotelName = result.HotelName,
            PhoneNumber = result.PhoneNumber,
            PostalCode = result.PostalCode,
            Rating = (int)result.Rating,
            State = result.State,
            Website = result.Website
        });
    }

    public override async Task<HotelDeleteResponse> DeleteAsync(HotelDeleteRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await hotelService.RemoveAsync(id);

        return await Task.FromResult(new HotelDeleteResponse
        {
            IsDelete = result
        });
    }

    public override async Task<HotelGetResponse> GetByIdAsync(HotelGetRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await hotelService.RetrieveByIdAsync(id);

        return await Task.FromResult(new HotelGetResponse
        {
            Id = result.Id,
            Address = result.Address,
            Capacity = result.Capacity,
            City = result.City,
            Country = result.Country,
            Description = result.Description,
            Email = result.Email,
            HotelName = result.HotelName,
            PhoneNumber = result.PhoneNumber,
            PostalCode = result.PostalCode,
            Rating = (int)result.Rating,
            State = result.State,
            Website = result.Website
        });
    }

    public override async Task<HotelGetAllResponse> GetAllAsync(HotelGetAllRequest request, ServerCallContext context)
    {
        var result = await hotelService.RetrieveAllAsync();

        var response = new HotelGetAllResponse();

        foreach (var item in result)
        {
            var hotel = new HotelGetResponse
            {
                Id = item.Id,
                Address = item.Address,
                Capacity = item.Capacity,
                City = item.City,
                Country = item.Country,
                Description = item.Description,
                Email = item.Email,
                HotelName = item.HotelName,
                PhoneNumber = item.PhoneNumber,
                PostalCode = item.PostalCode,
                Rating = (int)item.Rating,
                State = item.State,
                Website = item.Website
            };

            response.Sth.Add(hotel);
        }

        return response;
    }
}
