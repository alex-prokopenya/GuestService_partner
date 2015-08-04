namespace GuestService.Models.Excursion
{
    using GuestService;
    using GuestService.Data;
    using GuestService.Models;
    using System;
    using System.Runtime.CompilerServices;

    public class CategoryParam : BaseApiParam, IStartPointAndLanguageAndPriceOptionParam, IStartPointAndLanguageParam
    {
        public int? sp { get; set; }

        public string spa { get; set; }

        public int? StartPoint
        {
            get
            {
                return this.sp;
            }
        }

        public string StartPointAlias
        {
            get
            {
                return this.spa;
            }
        }

        public bool WithoutPrice
        {
            get
            {
                return (this.wp.HasValue ? this.wp.Value : !Settings.ExcursionWithPriceOnlyCatalog);
            }
        }

        public bool? wp { get; set; }
    }
}

