namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("GeoCatalogObjectList")]
    public class GeoCatalogObjectList : List<GeoCatalogObject>
    {
        public GeoCatalogObjectList(IEnumerable<GeoCatalogObject> collection) : base(collection)
        {
        }
    }
}

