using Mareen.Domain.Commons;
using Mareen.Domain.Enums;

namespace Mareen.Domain.Entities;

public sealed class PaymentHistory : Auditable
{
    public long GuestId { get; set; }
    public Guest Guest { get; set; }

    public long BookingId { get; set; }
    public Booking Booking { get; set; }

    public long PaymentId { get; set; }
    public Payment Payment { get; set; }

    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; } 
    public PaymentStatus PaymentStatus { get; set; }
}
