namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("ExcursionPickupHotelsList")]
    public class ExcursionPickupHotelsList : List<ExcursionPickupHotel>
    {
        public ExcursionPickupHotelsList(IEnumerable<ExcursionPickupHotel> collection) : base(collection)
        {
        }
    }
}

