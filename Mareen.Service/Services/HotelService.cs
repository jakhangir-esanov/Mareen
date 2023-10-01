using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.Hotels;
using Mareen.Service.DTOs.Rooms;
using Mareen.Service.DTOs.Users;
using Mareen.Service.Exceptions;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mareen.Service.Services;

public class HotelService : IHotelService
{
    private readonly IRepository<Hotel> repository;
    private readonly IMapper mapper;
    public HotelService(IRepository<Hotel> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<HotelResultDto> AddAsync(HotelCreationDto dto)
    {
        var hotel = await repository.SelectAsync(x => x.Email.Equals(dto.Email));
        if (hotel is not null)
            throw new AlreadyExistException("Already exist!");

        hotel = await repository.SelectNoFilterAsync(x => x.Email.Equals(dto.Email));
        if (hotel is not null)
        {
            mapper.Map(dto, hotel);
            repository.Update(hotel);
        }
        else
        {
            hotel = mapper.Map<Hotel>(dto);
            await repository.InsertAsync(hotel);
        }
        await repository.SaveAsync();

        var res = mapper.Map<HotelResultDto>(hotel);
        return res;
    }

    public async Task<HotelResultDto> ModifyAsync(HotelUpdateDto dto)
    {
        var hotel = await repository.SelectAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapHotel = mapper.Map(dto, hotel);
        repository.Update(mapHotel);
        await repository.SaveAsync();

        var res = mapper.Map<HotelResultDto>(mapHotel);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var hotel = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Delete(hotel);
        await repository.SaveAsync();

        return true;
    }

    public async Task<HotelResultDto> RetrieveByIdAsync(long id)
    {
        var hotel = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<HotelResultDto>(hotel);
        return res;
    }

    public async Task<IEnumerable<HotelResultDto>> RetrieveAllAsync()
    {
        var hotel = await repository.SelectAll().ToListAsync();
        var res = mapper.Map<IEnumerable<HotelResultDto>>(hotel);
        return res;
    }

    public async Task<IEnumerable<RoomResultDto>> RetrieveAllRoomsAsync(long hotelId)
    {
        Expression<Func<Hotel, bool>> expression = b => b.Id.Equals(hotelId);

        var hotel = await repository.SelectAsync(expression, new[] { "Room" })
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<IEnumerable<RoomResultDto>>(hotel.Rooms);
        return res;
    }

    public async Task<IEnumerable<UserResultDto>> RetrieveAllEmployeesAsync(long hotelId)
    {
        Expression<Func<Hotel, bool>> expression = b => b.Id.Equals(hotelId);

        var hotel = await repository.SelectAsync(expression, new[] { "User" })
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<IEnumerable<UserResultDto>>(hotel.Employees);
        return res;
    }
}
