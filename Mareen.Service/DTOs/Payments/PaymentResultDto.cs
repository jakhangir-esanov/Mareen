using Mareen.Domain.Entities;
using Mareen.Domain.Enums;

namespace Mareen.Service.DTOs.Payments;

public class PaymentResultDto
{
    public long Id { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public Booking Booking { get; set; }
    public Guest Guest { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}

