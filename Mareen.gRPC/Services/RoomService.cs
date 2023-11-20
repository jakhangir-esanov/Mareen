using Grpc.Core;
using Mareen.Service.DTOs.Rooms;
using Mareen.Service.Interfaces;

namespace Mareen.gRPC.Services;

public class RoomService : room.roomBase
{
    private readonly IRoomService roomService;

    public RoomService(IRoomService roomService)
    {
        this.roomService = roomService;
    }

    public override async Task<RoomCreationResponse> CreateAsync(RoomCreationRequest request, ServerCallContext context)
    {
        var room = new RoomCreationDto()
        {
            Description = request.Description,
            HotelId = request.HotelId,
            IsFree = request.IsFree,
            Price = request.Price,
            RoomNumber = request.RoomNumber,
            RoomType = (Domain.Enums.RoomType)request.RoomType
        };

        var result = await roomService.AddAsync(room);

        return await Task.FromResult(new RoomCreationResponse
        {
            Id = result.Id,
            Description = result.Description,
            HotelId = result.Hotel.Id,
            IsFree = result.IsFree, 
            Price = result.Price,
            RoomNumber = result.RoomNumber,
            RoomType = (int)result.RoomType
        });
    }

    public override async Task<RoomUpdateResponse> UpdateAsync(RoomUpdateRequest request, ServerCallContext context)
    {
        var room = new RoomUpdateDto()
        {
            Id = request.Id,
            Description = request.Description,
            HotelId = request.HotelId,
            IsFree = request.IsFree,
            Price = request.Price,
            RoomNumber = request.RoomNumber,
            RoomType = (Domain.Enums.RoomType)request.RoomType
        };

        var result = await roomService.ModifyAsync(room);

        return await Task.FromResult(new RoomUpdateResponse
        {
            Id = result.Id,
            Description = result.Description,
            HotelId = result.Hotel.Id,
            IsFree = result.IsFree,
            Price = result.Price,
            RoomNumber = result.RoomNumber,
            RoomType = (int)result.RoomType
        });
    }

    public override async Task<RoomDeleteResponse> DeleteAsync(RoomDeleteRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await roomService.RemoveAsync(id);

        return await Task.FromResult(new RoomDeleteResponse
        {
            IsDelete = result
        });
    }

    public override async Task<RoomGetResponse> GetByIdAsync(RoomGetRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await roomService.RetrieveByIdAsync(id);

        return await Task.FromResult(new RoomGetResponse
        {
            Id = result.Id,
            Description = result.Description,
            HotelId = result.Hotel.Id,
            IsFree = result.IsFree,
            Price = result.Price,
            RoomNumber = result.RoomNumber,
            RoomType = (int)result.RoomType
        });
    }

    public override async Task<RoomGetAllResponse> GetAllAsync(RoomGetAllRequest request, ServerCallContext context)
    {
        var result = await roomService.RetrieveAllAsync();

        var response = new RoomGetAllResponse();

        foreach (var item in result)
        {
            var room = new RoomGetResponse
            {
                Id = item.Id,
                Description = item.Description,
                HotelId = item.Hotel.Id,
                IsFree = item.IsFree,
                Price = item.Price,
                RoomNumber = item.RoomNumber,
                RoomType = (int)item.RoomType
            };

            response.Sth.Add(room);
        }

        return response;
    }

    public override async Task<RoomGetAllAvailableResponse> GetAllAvailableAsync(RoomGetAllAvailableRequest request, ServerCallContext context)
    {
        var result = await roomService.RetrieveAllAvailableAsync();

        var response = new RoomGetAllAvailableResponse();

        foreach (var item in result)
        {
            var room = new RoomGetResponse
            {
                Id = item.Id,
                Description = item.Description,
                HotelId = item.Hotel.Id,
                IsFree = item.IsFree,
                Price = item.Price,
                RoomNumber = item.RoomNumber,
                RoomType = (int)item.RoomType
            };

            response.Sth.Add(room);
        }

        return response;
    }
}
