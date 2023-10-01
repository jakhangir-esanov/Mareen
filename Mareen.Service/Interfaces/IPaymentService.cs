using Mareen.Service.DTOs.Payments;

namespace Mareen.Service.Interfaces;

public interface IPaymentService
{
    Task<PaymentResultDto> AddAsync(PaymentCreationDto dto);
    Task<PaymentResultDto> ModifyAsync(PaymentUpdateDto dto);
    Task<bool> RemoveAsync(long id);
    Task<PaymentResultDto> RetrieveByIdAsync(long id);
    Task<IEnumerable<PaymentResultDto>> RetrieveAllAsync();
}
