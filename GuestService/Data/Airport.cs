namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class Airport
    {
        public string alias { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public Town town { get; set; }
    }
}

