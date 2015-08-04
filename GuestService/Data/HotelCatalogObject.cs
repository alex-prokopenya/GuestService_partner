namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;

    public class HotelCatalogObject
    {
        public string address { get; set; }

        public string alias { get; set; }

        public GeoLocation geoposition { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public Region region { get; set; }

        public HotelStar star { get; set; }

        public Town town { get; set; }

        public string web { get; set; }
    }
}

