using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.BookingItems;
using Mareen.Service.DTOs.Bookings;
using Mareen.Service.Exceptions;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mareen.Service.Services;

public class BookingService : IBookingService
{
    private readonly IRepository<Booking> repository;
    private readonly IRepository<Room> repository1;
    private readonly IMapper mapper;
    public BookingService(IRepository<Booking> repository, IMapper mapper, IRepository<Room> repository1)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.repository1 = repository1;
    }

    public async Task<BookingResultDto> AddAsync(BookingCreationDto dto)
    {
        var booking = await repository.SelectAsync(x => x.GuestId.Equals(dto.GuestId));

        if (booking is not null)
            throw new AlreadyExistException("Already Exist!");

        var room = await repository1.SelectAsync(x => x.Id.Equals(dto.RoomId));
        if (room is null)
            throw new NotFoundException("Room doesn't exist!");
        else if (room.IsFree == false)
            throw new CustomException("Room already booked!");

        booking = await repository.SelectNoFilterAsync(x => x.GuestId.Equals(dto.GuestId));
        if (booking is not null)
        {
            mapper.Map(dto, booking);
            repository.Update(booking);
        }
        else
        {
            booking = mapper.Map<Booking>(dto);
            booking.Status = Domain.Enums.Status.inProgress;
            await repository.InsertAsync(booking);
        }
        room.IsFree = false;
        await repository.SaveAsync();

        var res = mapper.Map<BookingResultDto>(booking);
        return res;
    }

    public async Task<BookingResultDto> ModifyAsync(BookingUpdateDto dto)
    {
        var booking = await repository.SelectAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapBooking = mapper.Map(dto, booking);
        repository.Update(mapBooking);
        await repository.SaveAsync();

        var res = mapper.Map<BookingResultDto>(mapBooking);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var booking = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Delete(booking);
        await repository.SaveAsync();

        return true;
    }

    public async Task<BookingResultDto> RetrieveByIdAsync(long id)
    {
        var booking = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<BookingResultDto>(booking);
        return res;
    }

    public async Task<IEnumerable<BookingResultDto>> RetrieveAllAsync()
    {
        var booking = await repository.SelectAll()
            .Where(x => x.IsDeleted == false)
            .ToListAsync();

        var res = mapper.Map<IEnumerable<BookingResultDto>>(booking);
        return res;
    }

    public async Task<IEnumerable<BookingItemResultDto>> RetrieveAllBookingItemsAsync(long bookingId)
    {
        Expression<Func<Booking, bool>> expression = b => b.Id.Equals(bookingId);

        var booking = await repository.SelectAsync(expression, new[] { "BookingItem" })
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<IEnumerable<BookingItemResultDto>>(booking.BookingItems);
        return res;
    }
}
