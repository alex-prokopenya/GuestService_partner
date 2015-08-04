namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("ExcursionDateList")]
    public class ExcursionDateList : List<ExcursionDate>
    {
        public ExcursionDateList(IEnumerable<ExcursionDate> collection) : base(collection)
        {
        }
    }
}

