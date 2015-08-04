namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ExcursionPickup : GeoArea
    {
        [XmlAttribute]
        public string note { get; set; }

        [XmlElement("PickupTime")]
        public DateTime? pickuptime { get; set; }
    }
}

