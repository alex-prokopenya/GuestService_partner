namespace GuestService.Models.Guest
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using GuestService.Data;

    public class FindOrderContext
    {
        public List<GuestClaim> Claims { get; set; }

        public FindOrderModel Form { get; set; }

        public LinkOrderModel Link { get; set; }

        public bool NotFound { get; set; }
    }
}

