namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PriceSummary
    {
        [XmlAttribute]
        public string currency { get; set; }

        [XmlAttribute]
        public decimal price { get; set; }

        [XmlAttribute]
        public PriceType priceType { get; set; }

        public enum PriceType
        {
            perPerson,
            perService
        }
    }
}

