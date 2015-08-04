namespace GuestService.Models.Catalog
{
    using GuestService.Models;
    using System;
    using System.Runtime.CompilerServices;

    public class GeoPointByAliasParam : BaseApiParam
    {
        public string GeoPointAlias
        {
            get
            {
                return this.gpa;
            }
        }

        public string gpa { get; set; }
    }
}

