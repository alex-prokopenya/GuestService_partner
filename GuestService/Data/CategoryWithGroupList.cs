namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("CategoryWithGroupList")]
    public class CategoryWithGroupList : List<CategoryWithGroup>
    {
        public CategoryWithGroupList(IEnumerable<CategoryWithGroup> collection) : base(collection)
        {
        }
    }
}

