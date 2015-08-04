namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ExcursionDescriptionSection
    {
        [XmlArrayItem("Paragraph"), XmlArray("Paragraphs")]
        public List<string> paragraphs { get; set; }

        [XmlArray("Sections")]
        public List<ExcursionDescriptionSection> sections { get; set; }

        [XmlAttribute]
        public string title { get; set; }
    }
}

