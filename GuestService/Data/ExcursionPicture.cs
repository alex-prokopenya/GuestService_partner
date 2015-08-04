namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ExcursionPicture
    {
        [XmlAttribute]
        public string description { get; set; }

        [XmlAttribute]
        public int ex { get; set; }

        [XmlAttribute]
        public int index { get; set; }
    }
}

