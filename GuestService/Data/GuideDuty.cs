namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class GuideDuty : Guide
    {
        public List<GuideDutyTime> duties { get; set; }
    }
}

