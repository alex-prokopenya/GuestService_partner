namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BookingClaim
    {
        public Customer customer { get; set; }

        public string note { get; set; }

        public List<BookingOrder> orders { get; set; }

        public List<string> PromoCodes { get; set; }
    }
}

