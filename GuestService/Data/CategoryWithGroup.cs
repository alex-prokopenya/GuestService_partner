namespace GuestService.Data
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CategoryWithGroup : Category
    {
        [XmlElement("Group")]
        public CategoryGroup group { get; set; }
    }
}

