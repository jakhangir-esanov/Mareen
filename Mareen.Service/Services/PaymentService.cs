using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.PaymentHistories;
using Mareen.Service.DTOs.Payments;
using Mareen.Service.Exceptions;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mareen.Service.Services;

public class PaymentService : IPaymentService
{
    private readonly IRepository<Payment> repository;
    private readonly IMapper mapper;
    private readonly IPaymentHistoryService historyService;
    public PaymentService(IRepository<Payment> repository, IMapper mapper, IPaymentHistoryService historyService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this.historyService = historyService;
    }

    public async Task<PaymentResultDto> AddAsync(PaymentCreationDto dto)
    {
        var payment = await repository.SelectAsync(x => x.GuestId.Equals(dto.GuestId));
        if (payment is not null)
            if (payment.PaymentStatus == Domain.Enums.PaymentStatus.Success)
                throw new AlreadyExistException("Already exist!");

        payment = await repository.SelectNoFilterAsync(x => x.GuestId.Equals(dto.GuestId));
        if (payment is not null)
        {
            mapper.Map(dto, payment);
            repository.Update(payment);
        }
        else
        {
            payment = mapper.Map<Payment>(dto);
            payment.PaymentStatus = Domain.Enums.PaymentStatus.InProgress;
            await repository.InsertAsync(payment);
        }
        await repository.SaveAsync();

        PaymentHistoryCreationDto historyCreationDto = new PaymentHistoryCreationDto
        {
            PaymentId = payment.Id,
            Amount = dto.Amount,
            BookingId = dto.BookingId,
            GuestId = dto.GuestId,
            PaymentType = dto.PaymentType
        };
        await historyService.AddAsync(historyCreationDto);

        var res = mapper.Map<PaymentResultDto>(payment);
        return res;
    }

    public async Task<PaymentResultDto> ModifyAsync(PaymentUpdateDto dto)
    {
        var payment = await repository.SelectAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapPayment = mapper.Map(dto, payment);
        repository.Update(mapPayment);
        await repository.SaveAsync();

        var res = mapper.Map<PaymentResultDto>(mapPayment);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var payment = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Delete(payment);
        await repository.SaveAsync();

        return true;
    }

    public async Task<PaymentResultDto> RetrieveByIdAsync(long id)
    {
        var payment = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<PaymentResultDto>(payment);
        return res;
    }

    public async Task<IEnumerable<PaymentResultDto>> RetrieveAllAsync()
    {
        var payment = await repository.SelectAll().ToListAsync();
        var res = mapper.Map<IEnumerable<PaymentResultDto>>(payment);
        return res;
    }
}
