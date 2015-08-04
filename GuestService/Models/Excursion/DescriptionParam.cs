namespace GuestService.Models.Excursion
{
    using GuestService.Models;
    using System;
    using System.Runtime.CompilerServices;

    public class DescriptionParam : BaseApiParam
    {
        public int[] ex { get; set; }

        public int[] Excursions
        {
            get
            {
                return this.ex;
            }
        }
    }
}

