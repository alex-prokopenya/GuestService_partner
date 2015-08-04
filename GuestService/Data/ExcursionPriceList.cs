namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("ExcursionPriceList")]
    public class ExcursionPriceList : List<ExcursionPrice>
    {
        public ExcursionPriceList(IEnumerable<ExcursionPrice> collection) : base(collection)
        {
        }
    }
}

