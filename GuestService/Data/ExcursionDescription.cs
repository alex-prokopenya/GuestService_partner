namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ExcursionDescription
    {
        [XmlArray("Description")]
        public List<ExcursionDescriptionSection> description { get; set; }

        [XmlElement("Excursion")]
        public CatalogExcursion excursion { get; set; }

        [XmlArray("Pictures")]
        public List<ExcursionPicture> pictures { get; set; }
    }
}

