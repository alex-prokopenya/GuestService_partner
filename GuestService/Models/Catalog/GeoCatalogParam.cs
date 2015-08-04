namespace GuestService.Models.Catalog
{
    using GuestService.Models;
    using System;
    using System.Runtime.CompilerServices;

    public class GeoCatalogParam : BaseApiParam
    {
        public string s { get; set; }

        public string SearchText
        {
            get
            {
                return this.s;
            }
        }
    }
}

