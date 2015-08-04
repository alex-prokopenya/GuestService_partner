namespace GuestService.Models.Excursion
{
    using GuestService.Models;
    using System;
    using System.Runtime.CompilerServices;

    public class CatalogImageParam : ImageParam
    {
        public int? i { get; set; }

        public int Index
        {
            get
            {
                return (this.i.HasValue ? this.i.Value : 0);
            }
        }
    }
}

