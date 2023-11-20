using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using Mareen.Service.DTOs.Bookings;
using Mareen.Service.Interfaces;

namespace Mareen.gRPC.Services;

public class BookingService : booking.bookingBase
{
    private readonly IBookingService bookingService;
    public BookingService(IBookingService bookingService)
    {
        this.bookingService = bookingService;
    }

    public override async Task<BookingCreationResponse> CreateAsync(BookingCreationRequest request, ServerCallContext context)
    {
        var booking = new BookingCreationDto()
        {
            RoomId = request.RoomId,
            GuestId = request.GuestId,
            Amount = request.Amount,
            StartDate = request.StartDate.ToDateTime(),
            EndDate = request.EndDate.ToDateTime(),
            Status = (Domain.Enums.Status)request.Status
        };

        var result = await bookingService.AddAsync(booking);

        return await Task.FromResult(new BookingCreationResponse
        {
            Id = result.Id,
            Amount = result.Amount,
            RoomId = result.Room.Id,
            GuestId = result.Guest.Id,
            StartDate = result.StartDate.ToUniversalTime().ToTimestamp(),
            EndDate = result.EndDate.ToUniversalTime().ToTimestamp(),
            Status = (int)result.Status
        });
    }

    public override async Task<BookingUpdateResponse> UpdateAsync(BookingUpdateRequest request, ServerCallContext context)
    {
        var booking = new BookingUpdateDto()
        {
            Id = request.Id,
            RoomId = request.RoomId,
            GuestId = request.GuestId,
            Amount = request.Amount,
            StartDate = request.StartDate.ToDateTime(),
            EndDate = request.EndDate.ToDateTime(),
            Status = (Domain.Enums.Status)request.Status
        };

        var result = await bookingService.ModifyAsync(booking);

        return await Task.FromResult(new BookingUpdateResponse
        {
            Id = result.Id,
            Amount = result.Amount,
            RoomId = result.Room.Id,
            GuestId = result.Guest.Id,
            StartDate = result.StartDate.ToUniversalTime().ToTimestamp(),
            EndDate = result.EndDate.ToUniversalTime().ToTimestamp(),
            Status = (int)result.Status
        });
    }

    public override async Task<BookingDeleteResponse> DeleteAsync(BookingDeleteRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await bookingService.RemoveAsync(id);

        return await Task.FromResult(new BookingDeleteResponse
        {
            IsDelete = result
        });
    }

    public override async Task<BookingGetResponse> GetByIdAsync(BookingGetRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await bookingService.RetrieveByIdAsync(id);

        return await Task.FromResult(new BookingGetResponse
        {
            Id = result.Id,
            Amount = result.Amount,
            RoomId = result.Room.Id,
            GuestId = result.Guest.Id,
            StartDate = result.StartDate.ToUniversalTime().ToTimestamp(),
            EndDate = result.EndDate.ToUniversalTime().ToTimestamp(),
            Status = (int)result.Status
        });
    }

    public override async Task<BookingGetAllResponse> GetAllAsync(BookingGetAllResponse request, ServerCallContext context)
    {
        var result = await bookingService.RetrieveAllAsync();

        var response = new BookingGetAllResponse();

        foreach (var item in result)
        {
            var booking = new BookingGetResponse
            {   
                Id = item.Id,
                Amount = item.Amount,
                RoomId = item.Room.Id,
                GuestId = item.Guest.Id,
                StartDate = item.StartDate.ToUniversalTime().ToTimestamp(),
                EndDate = item.EndDate.ToUniversalTime().ToTimestamp(),
                Status = (int)item.Status
            };
            response.Sth.Add(booking);
        }

        return response;
    }

    public override async Task<BookingGetAllBookingItemsResponse> GetAllBookingItemsAsync(BookingGetAllBookingItemsRequest request, ServerCallContext context)
    {
        long bookingId = request.Id;

        var result = await bookingService.RetrieveAllBookingItemsAsync(bookingId);

        var response = new BookingGetAllBookingItemsResponse();

        foreach(var item in result)
        {
            var bookingItem = new BookingGetBookingItemsResponse
            {
                Id = item.Id,
                BookingId = bookingId,
                Quantity = item.Quantity,
                ServiceId = item.Service.Id
            };

            response.Sth.Add(bookingItem);
        }

        return response;
    }
}
