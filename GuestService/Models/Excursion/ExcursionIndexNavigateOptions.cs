namespace GuestService.Models.Excursion
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExcursionIndexNavigateOptions
    {
        public int[] categories { get; set; }

        public DateTime? date { get; set; }

        public int[] destinations { get; set; }

        public int? excursion { get; set; }

        public int[] languages { get; set; }

        public string sortorder { get; set; }

        public string text { get; set; }
    }
}

