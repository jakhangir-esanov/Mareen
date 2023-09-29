using Mareen.Domain.Entities;
using Mareen.Domain.Enums;

namespace Mareen.Service.DTOs.PaymentHistories;

public class PaymentHistoryResultDto
{
    public long Id { get; set; }
    public Guest Guest { get; set; }
    public Booking Booking { get; set; }
    public Payment Payment { get; set; }
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string Comment { get; set; }
}

