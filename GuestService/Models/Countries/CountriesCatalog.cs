using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GuestService.Models.Countries
{
    public class CountriesCatalog: SeoObject
    {
        public KeyValuePair<string, string>[] Countries { get; set; }
    }
}
