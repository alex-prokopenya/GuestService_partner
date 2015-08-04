namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("ExcursionDescriptionList")]
    public class ExcursionDescriptionList : List<ExcursionDescription>
    {
        public ExcursionDescriptionList(IEnumerable<ExcursionDescription> collection) : base(collection)
        {
        }
    }
}

