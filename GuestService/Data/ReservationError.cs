namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class ReservationError
    {
        public ReservationErrorType errortype { get; set; }

        public bool isstop { get; set; }

        public string message { get; set; }

        public int number { get; set; }

        public string orderid { get; set; }

        public string usermessage { get; set; }
    }
}

