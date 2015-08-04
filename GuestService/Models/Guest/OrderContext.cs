namespace GuestService.Models.Guest
{
    using GuestService.Data;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class OrderContext
    {
        public ReservationState Claim { get; set; }

        public bool ClaimsNotFound { get; set; }

        public List<ExcursionTransfer> ExcursionTransfers { get; set; }

        public OrderModel OrderFindForm { get; set; }

        public bool OrderFindNotFound { get; set; }

        public List<GuestClaim> OtherClaims { get; set; }

        public bool ShowOrderFindForm { get; set; }

        public bool ShowOtherClaims { get; set; }

        public UnlinkOrderModel Unlink { get; set; }
    }
}

