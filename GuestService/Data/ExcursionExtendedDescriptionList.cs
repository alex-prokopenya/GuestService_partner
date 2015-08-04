namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("ExcursionExtendedDescriptionList")]
    public class ExcursionExtendedDescriptionList : List<ExcursionExtendedDescription>
    {
        public ExcursionExtendedDescriptionList()
        {
        }

        public ExcursionExtendedDescriptionList(IEnumerable<ExcursionExtendedDescription> collection) : base(collection)
        {
        }
    }
}

