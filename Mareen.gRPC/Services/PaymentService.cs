using Grpc.Core;
using Mareen.Service.DTOs.Payments;
using Mareen.Service.Interfaces;

namespace Mareen.gRPC.Services;

public class PaymentService : payment.paymentBase
{
    private readonly IPaymentService paymentService;

    public PaymentService(IPaymentService paymentService)
    {
        this.paymentService = paymentService;
    }

    public override async Task<PaymentCreationResponse> CreateAsync(PaymentCreationRequest request, ServerCallContext context)
    {
        var payment = new PaymentCreationDto()
        {
            Amount = request.Amount,
            BookingId = request.BookingId,
            GuestId = request.GuestId,
            PaymentStatus = (Domain.Enums.PaymentStatus)request.PaymentStatus,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType   
        };

        var result = await paymentService.AddAsync(payment);

        return await Task.FromResult(new PaymentCreationResponse
        {
            Id = result.Id,
            Amount = result.Amount,
            BookingId = result.Booking.Id,
            GuestId = result.Guest.Id,
            PaymentStatus = (int)result.PaymentStatus,
            PaymentType= (int)result.PaymentType
        });
    }

    public override async Task<PaymentUpdateResponse> UpdateAsync(PaymentUpdateRequest request, ServerCallContext context)
    {
        var payment = new PaymentUpdateDto()
        {
            Id= request.Id,
            Amount = request.Amount,
            BookingId = request.BookingId,
            GuestId = request.GuestId,
            PaymentStatus = (Domain.Enums.PaymentStatus)request.PaymentStatus,
            PaymentType = (Domain.Enums.PaymentType)request.PaymentType
        };

        var result = await paymentService.ModifyAsync(payment);

        return await Task.FromResult(new PaymentUpdateResponse
        {
            Id = result.Id,
            Amount = result.Amount,
            BookingId = result.Booking.Id,
            GuestId = result.Guest.Id,
            PaymentStatus = (int)result.PaymentStatus,
            PaymentType = (int)result.PaymentType
        });
    }

    public override async Task<PaymentDeleteResponse> DeleteAsync(PaymentDeleteRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await paymentService.RemoveAsync(id);

        return await Task.FromResult(new PaymentDeleteResponse
        {
            IsDelete = result
        });
    }

    public override async Task<PaymentGetResponse> GetByIdAsync(PaymentGetRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await paymentService.RetrieveByIdAsync(id);

        return await Task.FromResult(new PaymentGetResponse
        {
            Id = result.Id,
            Amount = result.Amount,
            BookingId = result.Booking.Id,
            GuestId = result.Guest.Id,
            PaymentStatus = (int)result.PaymentStatus,
            PaymentType = (int)result.PaymentType
        });
    }

    public override async Task<PaymentGetAllResponse> GetAllAsync(PaymentGetAllRequest request, ServerCallContext context)
    {
        var result = await paymentService.RetrieveAllAsync();

        var response = new PaymentGetAllResponse();

        foreach (var item in result)
        {
            var payment = new PaymentGetResponse
            {
                Id = item.Id,
                Amount = item.Amount,
                BookingId = item.Booking.Id,
                GuestId = item.Guest.Id,
                PaymentStatus = (int)item.PaymentStatus,
                PaymentType = (int)item.PaymentType
            };

            response.Sth.Add(payment);
        }

        return response;
    }
}
