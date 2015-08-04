namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SearchGeography
    {
        [XmlAttribute]
        public int[] destinations { get; set; }

        [XmlAttribute]
        public string geotype { get; set; }

        [XmlAttribute]
        public string name { get; set; }
    }
}

