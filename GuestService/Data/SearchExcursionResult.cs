namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class SearchExcursionResult
    {
        [XmlArray("Geographies")]
        public List<SearchGeography> geography { get; set; }
    }
}

