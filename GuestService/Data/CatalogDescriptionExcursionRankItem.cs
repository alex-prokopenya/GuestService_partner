namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CatalogDescriptionExcursionRankItem
    {
        [XmlAttribute("name")]
        public string name { get; set; }

        [XmlAttribute("rank")]
        public decimal? rank { get; set; }
    }
}

