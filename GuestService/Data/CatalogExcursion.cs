namespace GuestService.Data
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml;
    using System.Xml.Serialization;

    public class CatalogExcursion
    {
        [XmlArray("Categories")]
        public List<ExcursionCategory> categories { get; set; }

        [XmlArray("Departures")]
        public List<GeoArea> departures { get; set; }

        [XmlArray("Destinations")]
        public List<GeoArea> destinations { get; set; }

        [XmlIgnore]
        public TimeSpan? duration { get; set; }

        [XmlAttribute("duration"), JsonIgnore]
        public string DurationString
        {
            get
            {
                return (!this.duration.HasValue ? null : XmlConvert.ToString(this.duration.Value));
            }
            set
            {
                this.duration = string.IsNullOrEmpty(value) ? null : new TimeSpan?(XmlConvert.ToTimeSpan(value));
            }
        }

        [XmlElement("ExcursionPartner")]
        public Partner excursionPartner { get; set; }

        [XmlAttribute]
        public int id { get; set; }

        [XmlArray("Languages")]
        public List<Language> languages { get; set; }

        [XmlAttribute]
        public string name { get; set; }

        [XmlAttribute]
        public string url { get; set; }
    }
}

