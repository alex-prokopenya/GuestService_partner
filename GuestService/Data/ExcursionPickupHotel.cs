namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ExcursionPickupHotel
    {
        [XmlAttribute]
        public int id { get; set; }

        [XmlAttribute]
        public string name { get; set; }

        [XmlAttribute]
        public string pickupplace { get; set; }

        [XmlAttribute]
        public DateTime? pickuptime { get; set; }

        [XmlAttribute]
        public string star { get; set; }

        [XmlAttribute]
        public int? strstar { get; set; }

        [XmlAttribute]
        public string town { get; set; }
    }
}

