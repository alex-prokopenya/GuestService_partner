namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CatalogFilterItem
    {
        [XmlAttribute]
        public int count { get; set; }

        [XmlAttribute]
        public int id { get; set; }

        [XmlAttribute]
        public string name { get; set; }
    }
}

