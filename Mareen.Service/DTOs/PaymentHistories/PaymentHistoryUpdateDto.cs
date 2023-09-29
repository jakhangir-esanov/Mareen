using Mareen.Domain.Enums;

namespace Mareen.Service.DTOs.PaymentHistories;

public class PaymentHistoryUpdateDto
{
    public long Id { get; set; }
    public long GuestId { get; set; }
    public long BookingId { get; set; }
    public long PaymentId { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string Comment { get; set; }
}

