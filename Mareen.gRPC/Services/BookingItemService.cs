using Grpc.Core;
using Mareen.Service.DTOs.BookingItems;
using Mareen.Service.Interfaces;

namespace Mareen.gRPC.Services;

public class BookingItemService : bookingItem.bookingItemBase
{
    private readonly IBookingItemService bookingItemService;

    public BookingItemService(IBookingItemService bookingItemService)
    {
        this.bookingItemService = bookingItemService;
    }

    public override async Task<BookingItemCreationResponse> CreateAsync(BookingItemCreationRequest request, ServerCallContext context)
    {
        var bookingItem = new BookingItemCreationDto()
        {
            BookingId = request.BookingId,
            Quantity = request.Quantity,
            ServiceId = request.ServiceId
        };

        var result = await bookingItemService.AddAsync(bookingItem);

        return await Task.FromResult(new BookingItemCreationResponse
        {
            Id = result.Id,
            BookingId = result.Booking.Id,
            Quantity = result.Quantity,
            ServiceId = result.Service.Id
        });
    }

    public override async Task<BookingItemUpdateResponse> UpdateAsync(BookingItemUpdateRequest request, ServerCallContext context)
    {
        var bookingItem = new BookingItemUpdateDto()
        {
            Id = request.Id,
            BookingId = request.BookingId,
            Quantity = request.Quantity,
            ServiceId = request.ServiceId
        };

        var result = await bookingItemService.ModifyAsync(bookingItem);

        return await Task.FromResult(new BookingItemUpdateResponse
        {
            Id = result.Id,
            BookingId = result.Booking.Id,
            Quantity = result.Quantity,
            ServiceId = result.Service.Id
        });
    }

    public override async Task<BookingItemDeleteResponse> DeleteAsync(BookingItemDeleteRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await bookingItemService.RemoveAsync(id);

        return await Task.FromResult(new BookingItemDeleteResponse
        {
            IsDelete = result
        });
    }

    public override async Task<BookingItemGetResponse> GetByIdAsync(BookingItemGetRequest request, ServerCallContext context)
    {
        long id = request.Id;

        var result = await bookingItemService.RetrieveByIdAsync(id);

        return await Task.FromResult(new BookingItemGetResponse
        {
            Id = result.Id,
            BookingId = result.Booking.Id,
            Quantity = result.Quantity,
            ServiceId = result.Service.Id
        });
    }

    public override async Task<BookingItemGetAllResponse> GetAllAsync(BookingItemGetAllRequest request, ServerCallContext context)
    {
        var result = await bookingItemService.RetrieveAllAsync();

        var response = new BookingItemGetAllResponse();

        foreach (var item in result)
        {
            var booking = new BookingItemGetResponse
            {
                Id = item.Id,
                BookingId = item.Booking.Id,
                Quantity = item.Quantity,
                ServiceId = item.Service.Id
            };

            response.Sth.Add(booking);
        }

        return response;
    }
}
