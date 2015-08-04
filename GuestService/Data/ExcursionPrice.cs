namespace GuestService.Data
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml;
    using System.Xml.Serialization;

    public class ExcursionPrice
    {
        [XmlIgnore]
        public DateTime? closesaletime { get; set; }

        [JsonIgnore, XmlAttribute("closesaletime")]
        public string ClosesaletimeString
        {
            get
            {
                return (!this.closesaletime.HasValue ? null : XmlConvert.ToString(this.closesaletime.Value, XmlDateTimeSerializationMode.Unspecified));
            }
            set
            {
                this.closesaletime = string.IsNullOrEmpty(value) ? null : new DateTime?(XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.Unspecified));
            }
        }

        [XmlAttribute]
        public DateTime date { get; set; }

        [XmlElement("Departures")]
        public List<GeoArea> departures { get; set; }

        [XmlElement("FreeSeats")]
        public int? freeseats { get; set; }

        [XmlElement("Group")]
        public ExcursionGroup group { get; set; }

        [XmlAttribute]
        public int id { get; set; }

        [XmlAttribute]
        public bool issaleclosed { get; set; }

        [XmlAttribute]
        public bool isstopsale { get; set; }

        [XmlElement("Language")]
        public Language language { get; set; }

        [XmlElement("PickupPoints")]
        public List<ExcursionPickup> pickuppoints { get; set; }

        [XmlElement("Price")]
        public PriceDetails price { get; set; }

        [XmlElement("Time")]
        public ExcursionTime time { get; set; }
    }
}

