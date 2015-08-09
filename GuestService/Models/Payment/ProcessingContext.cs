namespace GuestService.Models.Payment
{
    using GuestService.Data;
    using System;
    using System.Runtime.CompilerServices;

    public class ProcessingContext
    {
        public PaymentBeforeProcessingResult BeforePaymentResult { get; set; }

        public GuestService.Data.PaymentMode PaymentMode { get; set; }

        public ReservationState Reservation { get; set; }

        public string RedirectUrl { get; set; }
    }
}

