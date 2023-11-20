using Grpc.Core;
using Mareen.Service.DTOs.PaymentHistories;
using Mareen.Service.Interfaces;

namespace Mareen.gRPC.Services;

public class PaymentHistoryService : paymentHistory.paymentHistoryBase
{
    private readonly IPaymentHistoryService paymentHistoryService;

    public PaymentHistoryService(IPaymentHistoryService paymentHistoryService)
    {
        this.paymentHistoryService = paymentHistoryService;
    }

    public override async Task<PaymentHistoryCreationResponse> CreateAsync(PaymentHistoryCreationRequest request, ServerCallContext context)
    {
        var paymentHistory = new PaymentHistoryCreationDto()
        {
            Amount = request.Amount,
            BookingId = request.BookingId,
            GuestId = request.GuestId,
            PaymentId = request.PaymentId,
            PaymentStatus = (Domain.Enums.PaymentStatus)request.PaymentStatus,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType
        };

        var result = await paymentHistoryService.AddAsync(paymentHistory);

        return await Task.FromResult(new PaymentHistoryCreationResponse
        {
            Id = result.Id,
            Amount = result.Amount,
            BookingId = result.Booking.Id,
            PaymentId = result.Payment.Id,
            PaymentStatus = (int)result.PaymentStatus,
            PaymentType = (int)result.PaymentType
        });
    }

    public override async Task<PaymentHistoryUpdateResponse> UpdateAsync(PaymentHistoryUpdateRequest request, ServerCallContext context)
    {
        var paymentHistory = new PaymentHistoryUpdateDto()
        {
            Id = request.Id,
            Amount = request.Amount,
            BookingId = request.BookingId,
            PaymentId = request.PaymentId,
            GuestId= request.GuestId,
            PaymentStatus = (Domain.Enums.PaymentStatus)request.PaymentStatus,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType
        };

        var result = await paymentHistoryService.ModifyAsync(paymentHistory);

        return await Task.FromResult(new PaymentHistoryUpdateResponse
        {
            Id = result.Id,
            Amount = result.Amount,
            BookingId = result.Booking.Id,
            PaymentId = result.Payment.Id,
            GuestId = result.Guest.Id,
            PaymentStatus = (int)result.PaymentStatus,
            PaymentType = (int)result.PaymentType
        });
    }

    public override async Task<PaymentHistoryDeleteResponse> DeleteAsync(PaymentHistoryDeleteRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await paymentHistoryService.RemoveAsync(id);

        return await Task.FromResult(new PaymentHistoryDeleteResponse
        {
            IsDelete = result
        });
    }

    public override async Task<PaymentHistoryGetResponse> GetByIdAsync(PaymentHistoryGetRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await paymentHistoryService.RetrieveByIdAsync(id);

        return await Task.FromResult(new PaymentHistoryGetResponse
        {
            Id = result.Id,
            Amount = result.Amount,
            BookingId = result.Booking.Id,
            PaymentId = result.Payment.Id,
            GuestId = result.Guest.Id,
            PaymentStatus = (int)result.PaymentStatus,
            PaymentType = (int)result.PaymentType
        });
    }

    public override async Task<PaymentHistoryGetAllResponse> GetAllAsync(PaymentHistoryGetAllRequest request, ServerCallContext context)
    {
        var result = await paymentHistoryService.RetrieveAllAsync();

        var response = new PaymentHistoryGetAllResponse();

        foreach (var item in result)
        {
            var paymentHistory = new PaymentHistoryGetResponse
            {
                Id = item.Id,
                Amount = item.Amount,
                BookingId = item.Booking.Id,
                PaymentId = item.Payment.Id,
                GuestId = item.Guest.Id,
                PaymentStatus = (int)item.PaymentStatus,
                PaymentType = (int)item.PaymentType
            };

            response.Sth.Add(paymentHistory);
        }

        return response;
    }
}
