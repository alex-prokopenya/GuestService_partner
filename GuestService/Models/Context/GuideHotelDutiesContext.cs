namespace GuestService.Models.Context
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using GuestService.Data;

    public class GuideHotelDutiesContext
    {
        public GuideHotelDutiesContext()
        {
            this.Hotels = new List<HotelGuideResult>();
        }

        public List<HotelGuideResult> Hotels { get; private set; }
    }
}

