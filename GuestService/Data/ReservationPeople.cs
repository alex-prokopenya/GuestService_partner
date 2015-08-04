namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class ReservationPeople
    {
        public string address { get; set; }

        public int? age { get; set; }

        public string agetype { get; set; }

        public DateTime? born { get; set; }

        public string comment { get; set; }

        public string email { get; set; }

        public string gender { get; set; }

        public string id { get; set; }

        public int? localid { get; set; }

        public string name { get; set; }

        public ReservationPeoplePassport passport { get; set; }

        public string phone { get; set; }
    }
}

