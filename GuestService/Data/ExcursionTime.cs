namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class ExcursionTime
    {
        [XmlAttribute]
        public int id { get; set; }

        [XmlAttribute]
        public string name { get; set; }
    }
}

