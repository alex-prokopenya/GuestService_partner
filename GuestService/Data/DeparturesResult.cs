namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class DeparturesResult
    {
        [XmlArray("Departures")]
        public List<GeoArea> departures { get; set; }
    }
}

