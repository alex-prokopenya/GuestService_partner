namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("CategoryGroupWithCategoriesList")]
    public class CategoryGroupWithCategoriesList : List<CategoryGroupWithCategories>
    {
        public CategoryGroupWithCategoriesList(IEnumerable<CategoryGroupWithCategories> collection) : base(collection)
        {
        }
    }
}

