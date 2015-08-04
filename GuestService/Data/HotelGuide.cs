namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelGuide
    {
        public List<HotelGuideDurty> durties { get; set; }

        public int id { get; set; }

        public string mobile { get; set; }

        public string name { get; set; }
    }
}

