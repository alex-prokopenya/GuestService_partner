namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CatalogFilterLocationItem : CatalogFilterItem
    {
        [XmlElement("Location")]
        public GeoLocation location { get; set; }
    }
}

