namespace GuestService.Models.Guest
{
    using GuestService.Data;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SummaryContext
    {
        public ReservationState Claim { get; set; }

        public List<HotelGuideResult> GuideDurties { get; set; }

        public List<DepartureHotel> Hotels { get; set; }

        public OrderModel OrderFindForm { get; set; }

        public bool OrderFindNotFound { get; set; }

        public bool ShowOrderFindForm { get; set; }
    }
}

