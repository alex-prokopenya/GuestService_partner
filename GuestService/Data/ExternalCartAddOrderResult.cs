namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExternalCartAddOrderResult
    {
        public int errorcode { get; set; }

        public string errormessage { get; set; }

        public int? ordercount { get; set; }
    }
}

