namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CatalogExcursionMinPrice
    {
        [XmlElement("Excursion")]
        public CatalogExcursion excursion { get; set; }

        [XmlElement("MinPrice")]
        public PriceSummary minPrice { get; set; }

        [XmlElement("Rank")]
        public CatalogExcursionRanking ranking { get; set; }
    }
}

