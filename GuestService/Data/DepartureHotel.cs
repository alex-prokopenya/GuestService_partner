namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DepartureHotel
    {
        public int id { get; set; }

        public string name { get; set; }

        public List<DepartureTransfer> transfers { get; set; }
    }
}

