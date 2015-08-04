namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class PaymentMode
    {
        public ReservationOrderPrice comission { get; set; }

        public string id { get; set; }

        public string name { get; set; }

        public string paymentparam { get; set; }

        public ReservationOrderPrice payrest { get; set; }

        public string processing { get; set; }
    }
}

