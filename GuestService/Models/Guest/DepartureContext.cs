namespace GuestService.Models.Guest
{
    using GuestService.Data;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DepartureContext
    {
        public HotelCatalogObject Hotel { get; set; }

        public List<DepartureHotel> Hotels { get; set; }
    }
}

