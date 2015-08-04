namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class CheckPromoCodeResult
    {
        public string code { get; set; }

        public int errorcode { get; set; }

        public string errormessage { get; set; }
    }
}

