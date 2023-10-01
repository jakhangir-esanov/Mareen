using Mareen.Domain.Enums;

namespace Mareen.Service.DTOs.Payments;

public class PaymentCreationDto
{
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public long BookingId { get; set; }
    public long GuestId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}

