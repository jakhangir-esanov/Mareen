using Mareen.Service.DTOs.PaymentHistories;

namespace Mareen.Service.Interfaces;

public interface IPaymentHistoryService
{
    Task<PaymentHistoryResultDto> AddAsync(PaymentHistoryCreationDto dto);
    Task<PaymentHistoryResultDto> ModifyAsync(PaymentHistoryUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<PaymentHistoryResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<PaymentHistoryResultDto>> RetrieveAllAsync();
}
