namespace GuestService
{
    using System;
    using System.Runtime.CompilerServices;

    public class PaymentPaypalSettings
    {
        public bool IsSandbox { get; set; }

        public string Password { get; set; }

        public string Signature { get; set; }

        public string Username { get; set; }
    }
}

