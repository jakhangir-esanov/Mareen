﻿using Mareen.Domain.Commons;
using Mareen.Domain.Enums;

namespace Mareen.Domain.Entities;

public sealed class Payment : Auditable
{
    public double Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public long BookingId { get; set; }
    public Booking Booking { get; set; }
}
