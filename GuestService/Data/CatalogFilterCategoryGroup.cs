namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CatalogFilterCategoryGroup
    {
        [XmlArray("Items")]
        public List<CatalogFilterItem> items { get; set; }

        [XmlAttribute]
        public string name { get; set; }
    }
}

