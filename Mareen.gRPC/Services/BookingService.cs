using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Mareen.Service.DTOs.Bookings;
using Mareen.Service.Interfaces;

namespace Mareen.gRPC.Services;

public class BookingService : booking.bookingBase
{
    private readonly IBookingService bookingService;
    private readonly IMapper mapper;
    public BookingService(IBookingService bookingService, IMapper mapper)
    {
        this.bookingService = bookingService;
        this.mapper = mapper;
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
            StartDate = result.StartDate.ToTimestamp(), 
            EndDate = result.EndDate.ToTimestamp(),     
            Status = (int)result.Status
        });
    }
}
