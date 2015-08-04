namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ExcursionDate
    {
        [XmlAttribute]
        public bool allclosed { get; set; }

        [XmlAttribute]
        public DateTime date { get; set; }

        [XmlAttribute]
        public bool isprice { get; set; }

        [XmlAttribute]
        public bool isstopsale { get; set; }
    }
}

