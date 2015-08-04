namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class Customer
    {
        public string address { get; set; }

        public string email { get; set; }

        public string mobile { get; set; }

        public string name { get; set; }

        public CustomerPassport passport { get; set; }
    }
}

