using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.Bookings;
using Mareen.Service.DTOs.Guests;
using Mareen.Service.DTOs.PaymentHistories;
using Mareen.Service.Exceptions;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mareen.Service.Services;

public class GuestService : IGuestService
{
    private readonly IRepository<Guest> repository;
    private readonly IMapper mapper;
    public GuestService(IRepository<Guest> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<GuestResultDto> AddAsync(GuestCreationDto dto)
    {
        var guest = await repository.SelectAsync(x => x.Email.Equals(dto.Email));
        if (guest is not null)
            throw new AlreadyExistException("Already exist!");

        guest = await repository.SelectNoFilterAsync(x => x.Email.Equals(dto.Email));
        if (guest is not null)
        {
            mapper.Map(dto, guest);
            repository.Update(guest);
        }
        else
        {
            guest = mapper.Map<Guest>(dto);
            await repository.InsertAsync(guest);
        }
        await repository.SaveAsync();

        var res = mapper.Map<GuestResultDto>(guest);
        return res;
    }

    public async Task<GuestResultDto> ModifyAsync(GuestUpdateDto dto)
    {
        var guest = await repository.SelectAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapGuest = mapper.Map(dto, guest);
        repository.Update(mapGuest);
        await repository.SaveAsync();

        var res = mapper.Map<GuestResultDto>(mapGuest);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var guest = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Delete(guest);
        await repository.SaveAsync();

        return true;
    }

    public async Task<GuestResultDto> RetrieveByIdAsync(long id)
    {
        var guest = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<GuestResultDto>(guest);
        return res;
    }

    public async Task<IEnumerable<GuestResultDto>> RetrieveAllAsync()
    {
        var guest = await repository.SelectAll().ToListAsync();
        var res = mapper.Map<IEnumerable<GuestResultDto>>(guest);
        return res;
    }

    public async Task<IEnumerable<PaymentHistoryResultDto>> RetrieveAllPaymentHistoriesAsync(long guestId)
    {
        Expression<Func<Guest, bool>> expression = x => x.Id.Equals(guestId);

        var guest = await repository.SelectAsync(expression, new[] { "PaymentHistory" })
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<IEnumerable<PaymentHistoryResultDto>>(guest.Transactions);
        return res;
    }

    public async Task<IEnumerable<BookingResultDto>> RetrieveAllBookingsAsync(long guestId)
    {
        Expression<Func<Guest, bool>> expression = b => b.Id.Equals(guestId);

        var guest = await repository.SelectAsync(expression, new[] { "Booking" })
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<IEnumerable<BookingResultDto>>(guest.Bookings);
        return res;
    }
}
