namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExcursionOrderCalculatePrice : ExcursionOrder
    {
        public DateTime? closesaletime { get; set; }

        public bool issaleclosed { get; set; }

        public bool isstopsale { get; set; }
    }
}

