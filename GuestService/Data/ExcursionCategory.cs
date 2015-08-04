namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ExcursionCategory
    {
        [XmlElement("CategoryGroup")]
        public CategoryGroup categorygroup { get; set; }

        [XmlAttribute]
        public int id { get; set; }

        [XmlAttribute]
        public string name { get; set; }

        [XmlIgnore]
        public int? sort { get; set; }
    }
}

