namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class GeoLocation
    {
        [XmlAttribute]
        public decimal latitude { get; set; }

        [XmlAttribute]
        public decimal longitude { get; set; }
    }
}

