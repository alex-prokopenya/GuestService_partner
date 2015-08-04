namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExcursionTransfer
    {
        public int? claim { get; set; }

        public DateTime date { get; set; }

        public string excursion { get; set; }

        public string excursiontime { get; set; }

        public int? exsale { get; set; }

        public DepartureWorker guide { get; set; }

        public DepartureWorker guide2 { get; set; }

        public int? order { get; set; }

        public DateTime? pickup { get; set; }

        public string pickupplace { get; set; }

        public string transferident { get; set; }

        public string transfernote { get; set; }

        public string voucher { get; set; }
    }
}

