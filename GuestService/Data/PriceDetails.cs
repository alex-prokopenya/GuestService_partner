namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class PriceDetails
    {
        [XmlAttribute]
        public decimal adult { get; set; }

        [XmlAttribute]
        public decimal child { get; set; }

        [XmlAttribute]
        public string currency { get; set; }

        [XmlAttribute]
        public decimal infant { get; set; }

        [XmlAttribute]
        public int maxGroup { get; set; }

        [XmlAttribute]
        public int minGroup { get; set; }

        [XmlAttribute]
        public PriceType priceType { get; set; }

        [XmlAttribute]
        public decimal service { get; set; }

        public enum PriceType
        {
            perPerson,
            perService
        }
    }
}

