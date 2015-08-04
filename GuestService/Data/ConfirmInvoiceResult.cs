namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConfirmInvoiceResult
    {
        public int? Error { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsSuccess
        {
            get
            {
                return !this.Error.HasValue;
            }
        }
    }
}

