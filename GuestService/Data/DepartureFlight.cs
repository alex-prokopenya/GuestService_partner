namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class DepartureFlight
    {
        public int id { get; set; }

        public TimeSpan? landingtime { get; set; }

        public string name { get; set; }

        public Airport source { get; set; }

        public DateTime? takeoff { get; set; }

        public Airport target { get; set; }
    }
}

