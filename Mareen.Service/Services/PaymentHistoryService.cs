using AutoMapper;
using Mareen.DAL.IRepositories;
using Mareen.Domain.Entities;
using Mareen.Service.DTOs.PaymentHistories;
using Mareen.Service.Exceptions;
using Mareen.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mareen.Service.Services;

public class PaymentHistoryService : IPaymentHistoryService
{
    private readonly IRepository<PaymentHistory> repository;
    private readonly IMapper mapper;
    public PaymentHistoryService(IRepository<PaymentHistory> repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

    public async Task<PaymentHistoryResultDto> AddAsync(PaymentHistoryCreationDto dto)
    {
        var paymentHistory = await repository.SelectAsync(x => x.PaymentId.Equals(dto.PaymentId));
        if (paymentHistory is not null)
            throw new AlreadyExistException("Already exist!");

        paymentHistory = await repository.SelectNoFilterAsync(x => x.PaymentId.Equals(dto.PaymentId));
        if (paymentHistory is not null)
        {
            mapper.Map(dto, paymentHistory);
            repository.Update(paymentHistory);
        }
        else
        {
            paymentHistory = mapper.Map<PaymentHistory>(dto);
            paymentHistory.PaymentStatus = Domain.Enums.PaymentStatus.InProgress;
            await repository.InsertAsync(paymentHistory);
        }
        await repository.SaveAsync();

        var res = mapper.Map<PaymentHistoryResultDto>(paymentHistory);
        return res;
    }

    public async Task<PaymentHistoryResultDto> ModifyAsync(PaymentHistoryUpdateDto dto)
    {
        var paymentHistory = await repository.SelectAsync(x => x.Id.Equals(dto.Id))
            ?? throw new NotFoundException("Not found!");

        var mapPaymentHistory = mapper.Map<PaymentHistory>(dto);
        repository.Update(mapPaymentHistory);
        await repository.SelectAsync();

        var res = mapper.Map<PaymentHistoryResultDto>(mapPaymentHistory);
        return res;
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var paymentHistory = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        repository.Delete(paymentHistory);
        await repository.SaveAsync();

        return true;
    }

    public async Task<PaymentHistoryResultDto> RetrieveByIdAsync(long id)
    {
        var paymentHistory = await repository.SelectAsync(x => x.Id.Equals(id))
            ?? throw new NotFoundException("Not found!");

        var res = mapper.Map<PaymentHistoryResultDto>(paymentHistory);
        return res;
    }

    public async Task<IEnumerable<PaymentHistoryResultDto>> RetrieveAllAsync()
    {
        var paymentHistory = await repository.SelectAll().ToListAsync();
        var res = mapper.Map<IEnumerable<PaymentHistoryResultDto>>(paymentHistory);
        return res;
    }
}
