using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.Attachments;
using Mareen.Service.DTOs.Bookings;
using Mareen.Service.DTOs.Guests;
using Mareen.Service.DTOs.PaymentHistories;
using Mareen.Service.Exceptions;
using Mareen.Service.Helpers;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Mareen.Service.Services;

public class GuestService : IGuestService
{
    private readonly IAttachmentService attachmentService;
    private readonly IRepository<Guest> repository;
    private readonly IMapper mapper;
    public GuestService(IRepository<Guest> repository, IMapper mapper, IAttachmentService attachmentService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.attachmentService = attachmentService;
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
            var hashResult = PasswordHasher.Hash(dto.Password);

            guest = mapper.Map<Guest>(dto);
            guest.Password = hashResult.Password;
            guest.Salt = hashResult.Salt;
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

    public async Task<GuestResultDto> ImageUploadAsync(long guestId, AttachmentCreationDto dto)
    {
        var guest = await this.repository.SelectAsync(x => x.Id.Equals(guestId))
            ?? throw new NotFoundException("Not found!");

        if (guest.AttachmentId is not null)
            throw new AlreadyExistException("Attachment already exist!");

        var createAttachment = await this.attachmentService.UploadAsync(dto);
        guest.AttachmentId = createAttachment.Id;
        guest.Attachment = createAttachment;

        this.repository.Update(guest);
        await this.repository.SaveAsync();

        return mapper.Map<GuestResultDto>(guest); 
    }

    public async Task<GuestResultDto> ModifyImageAsync(long guestId, AttachmentCreationDto dto)
    {
        var guest = await this.repository.SelectAsync(x => x.Id.Equals(guestId))
            ?? throw new NotFoundException("Not found!");

        long attachmentId = guest.AttachmentId
                   ?? throw new NotFoundException("Attachment was not found!");

        await this.attachmentService.RemoveAsync(attachmentId);

        var createAttachment = await this.attachmentService.UploadAsync(dto);
        guest.AttachmentId = createAttachment.Id;
        guest.Attachment = createAttachment;

        this.repository.Update(guest);
        await this.repository.SaveAsync();

        return mapper.Map<GuestResultDto>(guest);
    }
}
