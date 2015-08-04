namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class Language
    {
        [XmlAttribute]
        public string alias { get; set; }

        [XmlAttribute]
        public int id { get; set; }

        [XmlAttribute]
        public string name { get; set; }
    }
}

