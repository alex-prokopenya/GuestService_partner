namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class GeoArea
    {
        [XmlAttribute]
        public string alias { get; set; }

        [XmlAttribute]
        public int id { get; set; }

        [XmlElement("Location")]
        public GeoLocation location { get; set; }

        [XmlAttribute]
        public string name { get; set; }
    }
}

