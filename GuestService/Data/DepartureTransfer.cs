namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class DepartureTransfer
    {
        public DateTime date { get; set; }

        public DepartureFlight flight { get; set; }

        public DepartureWorker guide { get; set; }

        public DepartureWorker guide2 { get; set; }

        public DepartureDestinationHotel hotel { get; set; }

        public int id { get; set; }

        public string ident { get; set; }

        public bool indiviadual { get; set; }

        public string language { get; set; }

        public string note { get; set; }

        public List<DepartureMember> people { get; set; }

        public DateTime? pickup { get; set; }

        public string servicename { get; set; }
    }
}

