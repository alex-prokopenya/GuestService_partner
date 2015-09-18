using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuestService.Models.Countries
{
    public class CountryRegions: SeoObject
    {
        public string CountryName { get; set; }

        public string CountryId { get; set; }

        public KeyValuePair<string, string>[] Regions { get; set; }
    }
}
