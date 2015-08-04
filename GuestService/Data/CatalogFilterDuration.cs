namespace GuestService.Data
{
    using Newtonsoft.Json;
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml;
    using System.Xml.Serialization;

    public class CatalogFilterDuration
    {
        [XmlIgnore]
        public TimeSpan max { get; set; }

        [XmlAttribute(AttributeName="max"), JsonIgnore]
        public string MaxString
        {
            get
            {
                return XmlConvert.ToString(this.max);
            }
            set
            {
                this.max = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }

        [XmlIgnore]
        public TimeSpan min { get; set; }

        [XmlAttribute(AttributeName="min"), JsonIgnore]
        public string MinString
        {
            get
            {
                return XmlConvert.ToString(this.min);
            }
            set
            {
                this.min = string.IsNullOrEmpty(value) ? TimeSpan.Zero : XmlConvert.ToTimeSpan(value);
            }
        }
    }
}

