namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CatalogFilterLocationWithStateItem : CatalogFilterLocationItem
    {
        public CatalogFilterLocationWithStateItem()
        {
        }

        public CatalogFilterLocationWithStateItem(CatalogFilterLocationItem item, string stateId)
        {
            base.id = item.id;
            base.name = item.name;
            base.count = item.count;
            base.location = item.location;
            this.stateid = stateId;
        }

        [XmlAttribute]
        public string stateid { get; set; }
    }
}

