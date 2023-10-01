using Mareen.Domain.Enums;

namespace Mareen.Service.DTOs.Payments;

public class PaymentUpdateDto
{
    public long Id { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public long BookingId { get; set; }
    public long GuestId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}

