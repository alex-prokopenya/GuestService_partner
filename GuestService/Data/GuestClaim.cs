namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class GuestClaim
    {
        public int claim { get; set; }

        public List<GuestOrder> orders { get; set; }

        public DatePeriod period { get; set; }

        public string tourname { get; set; }
    }
}

