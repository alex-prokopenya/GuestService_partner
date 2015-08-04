namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class HotelGuideResult
    {
        public List<HotelGuide> guides { get; set; }

        public HotelCatalogObject hotel { get; set; }
    }
}

