namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class PaymentBeforeProcessingResult
    {
        public string invoiceNumber { get; set; }

        public bool success { get; set; }
    }
}

