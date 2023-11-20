using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using Mareen.Service.DTOs.Guests;
using Mareen.Service.Interfaces;

namespace Mareen.gRPC.Services;

public class GuestService : guest.guestBase
{
    private readonly IGuestService guestService;

    public GuestService(IGuestService guestService)
    {
        this.guestService = guestService;
    }

    public override async Task<GuestCreationResponse> CreateAsync(GuestCreationRequest request, ServerCallContext context)
    {
        var guest = new GuestCreationDto()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth.ToDateTime(),
            PassportDetails = request.PassportDetails,
            Password = request.Password
        };

        var result = await guestService.AddAsync(guest);

        return await Task.FromResult(new GuestCreationResponse
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            PhoneNumber = result.PhoneNumber,
            DateOfBirth = result.DateOfBirth.ToUniversalTime().ToTimestamp(),
            PassportDetails = result.PassportDetails,
            Password = result.Password
        });
    }

    public override async Task<GuestUpdateResponse> UpdateAsync(GuestUpdateRequest request, ServerCallContext context)
    {
        var guest = new GuestUpdateDto()
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth.ToDateTime(),
            PassportDetails = request.PassportDetails,
            Password = request.Password
        };

        var result = await guestService.ModifyAsync(guest);

        return await Task.FromResult(new GuestUpdateResponse
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            PhoneNumber = result.PhoneNumber,
            DateOfBirth = result.DateOfBirth.ToUniversalTime().ToTimestamp(),
            PassportDetails = result.PassportDetails,
            Password = result.Password
        });
    }

    public override async Task<GuestDeleteResponse> DeleteAsync(GuestDeleteRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await guestService.RemoveAsync(id);

        return await Task.FromResult(new GuestDeleteResponse
        {
            IsDelete = result
        });
    }

    public override async Task<GuestGetResponse> GetByIdAsync(GuestGetRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await guestService.RetrieveByIdAsync(id);

        return await Task.FromResult(new GuestGetResponse
        {
            Id = result.Id,
            FirstName = result.FirstName,
            LastName = result.LastName,
            Email = result.Email,
            PhoneNumber = result.PhoneNumber,
            DateOfBirth = result.DateOfBirth.ToUniversalTime().ToTimestamp(),
            PassportDetails = result.PassportDetails,
            Password = result.Password
        });
    }

    public override async Task<GuestGetAllResponse> GetAllAsync(GuestGetAllRequest request, ServerCallContext context)
    {
        var result = await guestService.RetrieveAllAsync();

        var response = new GuestGetAllResponse();

        foreach (var item in result)
        {
            var guest = new GuestGetResponse
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber,
                DateOfBirth = item.DateOfBirth.ToUniversalTime().ToTimestamp(),
                PassportDetails = item.PassportDetails,
                Password = item.Password
            };

            response.Sth.Add(guest);
        }

        return response;
    }

    public override async Task<GuestGetAllPaymentHistoriesResponse> GetAllPaymentHistoriesAsync(GuestGetAllPaymentHistoriesRequest request, ServerCallContext context)
    {
        long guestId = request.Id;

        var result = await guestService.RetrieveAllPaymentHistoriesAsync(guestId);

        var response = new GuestGetAllPaymentHistoriesResponse();

        foreach (var item in result)
        {
            var paymentHistory = new GuestGetPaymentHistoryResponse
            {
                Id = item.Id,
                Amount = item.Amount,
                BookingId = item.Booking.Id,
                GuestId = item.Guest.Id,
                PaymentId = item.Payment.Id,
                PaymentStatus = (int)item.PaymentStatus,
                PaymentType = (int)item.PaymentType
            };

            response.Sth.Add(paymentHistory);
        }

        return response;
    }

    public override async Task<GuestGetAllBookingsResponse> GetAllBookingsAsync(GuestGetAllBookingsRequest request, ServerCallContext context)
    {
        long guestId = request.Id;

        var result = await guestService.RetrieveAllBookingsAsync(guestId);

        var response = new GuestGetAllBookingsResponse();

        foreach (var item in result)
        {
            var booking = new GuestGetBookingResponse()
            {
                Id = item.Id,
                Amount = item.Amount,
                GuestId = item.Guest.Id,
                RoomId = item.Room.Id,
                StartDate = item.StartDate.ToUniversalTime().ToTimestamp(),
                EndDate = item.EndDate.ToUniversalTime().ToTimestamp(),
                Status = (int)item.Status
            };

            response.Sth.Add(booking);
        }

        return response;
    }
}
