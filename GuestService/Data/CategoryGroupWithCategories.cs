namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CategoryGroupWithCategories
    {
        [XmlArray("Categories")]
        public List<Category> categories { get; set; }

        [XmlElement("Group")]
        public CategoryGroup group { get; set; }
    }
}

