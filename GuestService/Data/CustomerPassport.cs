namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class CustomerPassport
    {
        public DateTime? issuedate { get; set; }

        public string issueplace { get; set; }

        public string number { get; set; }

        public string serie { get; set; }
    }
}

