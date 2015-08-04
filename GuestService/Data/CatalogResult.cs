namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CatalogResult
    {
        [XmlArray("Excursions")]
        public List<CatalogExcursionMinPrice> excursions { get; set; }
    }
}

