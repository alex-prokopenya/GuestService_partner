namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class GeoCatalogObject
    {
        [XmlAttribute]
        public string geotype { get; set; }

        [XmlAttribute]
        public int id { get; set; }

        [XmlAttribute]
        public string name { get; set; }
    }
}

