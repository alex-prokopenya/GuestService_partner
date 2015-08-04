namespace GuestService.Models.Guest
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using GuestService.Data;

    public class GuestContext
    {
        public List<HotelGuideResult> GuideDurties { get; set; }

        public bool ShowAuthenticationMessage { get; set; }
    }
}

