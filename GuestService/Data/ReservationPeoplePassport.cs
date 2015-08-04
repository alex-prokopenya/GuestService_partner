namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class ReservationPeoplePassport
    {
        public DateTime? issue { get; set; }

        public string number { get; set; }

        public string serie { get; set; }

        public DateTime? valid { get; set; }
    }
}

