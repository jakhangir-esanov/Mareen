using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.BookingItems;
using Mareen.Service.Exceptions;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mareen.Service.Services;

public class BookingItemService : IBookingItemService
{
    private readonly IRepository<BookingItem> repository;
    private readonly IMapper mapper;
    public BookingItemService(IRepository<BookingItem> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<BookingItemResultDto> AddAsync(BookingItemCreationDto dto)
    {
        var bookingItem = await repository.SelectAsync(x => x.BookingId.Equals(dto.BookingId));
        if (bookingItem is not null)
            throw new AlreadyExistException("Already Exist!");

        bookingItem = await repository.SelectNoFilterAsync(x => x.BookingId.Equals(dto.BookingId));
        if (bookingItem is not null)
        {
            mapper.Map(dto, bookingItem);
            repository.Update(bookingItem);
        }
        else
        {
            bookingItem = mapper.Map<BookingItem>(dto);
            await repository.InsertAsync(bookingItem);
        }
        await repository.SaveAsync();

        var res = mapper.Map<BookingItemResultDto>(bookingItem);
        return res;
    }

    public async Task<BookingItemResultDto> ModifyAsync(BookingItemUpdateDto dto)
    {
        var bookingItem = await repository.SelectAsync(x => x.Id.Equals(dto.Id))
              ?? throw new NotFoundException("Not found!");

        var mapBookingItem = mapper.Map(dto, bookingItem);
        repository.Update(mapBookingItem);
        await repository.SaveAsync();

        var res = mapper.Map<BookingItemResultDto>(mapBookingItem);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var bookingItem = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Delete(bookingItem);
        await repository.SaveAsync();

        return true;
    }

    public async Task<BookingItemResultDto> RetrieveByIdAsync(long id)
    {
        var bookingItem = await repository.SelectAsync(x => x.Id.Equals(id))
           ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<BookingItemResultDto>(bookingItem);
        return res;
    }

    public async Task<IEnumerable<BookingItemResultDto>> RetrieveAllAsync()
    {
        var bookingItem = await repository.SelectAll().ToListAsync();
        var res = mapper.Map<IEnumerable<BookingItemResultDto>>(bookingItem);
        return res;
    }
}
