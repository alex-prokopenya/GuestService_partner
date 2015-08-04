namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class DestinationsAndCategoryGroupsResult
    {
        [XmlArray("CategoryGroups")]
        public List<CategoryGroupWithCategories> categorygroups { get; set; }

        [XmlArray("DestinationStates")]
        public List<GeoArea> destinationstates { get; set; }
    }
}

