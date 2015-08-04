namespace GuestService.Models.Guest
{
    using System;
    using System.Runtime.CompilerServices;

    public class PrintOrderContext
    {
        public PrintOrderModel Form { get; set; }

        public bool NotFound { get; set; }
    }
}

