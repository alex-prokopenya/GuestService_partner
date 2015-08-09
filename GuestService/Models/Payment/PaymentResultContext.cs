namespace GuestService.Models.Payment
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PaymentResultContext
    {
        public PaymentResultContext()
        {
            this.Errors = new List<string>();
        }

        public List<string> Errors { get; private set; }

        public bool Success { get; set; }

        public string Order
        {
            get;
            set;
        }
    }
}

