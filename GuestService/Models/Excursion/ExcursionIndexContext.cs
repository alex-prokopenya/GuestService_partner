namespace GuestService.Models.Excursion
{
    using System;
    using System.Runtime.CompilerServices;

    public class ExcursionIndexContext: SeoObject
    {
        public DateTime ExcursionDate { get; set; }

        public string ExternalCartId { get; set; }

        public ExcursionIndexNavigateCommand NavigateState { get; set; }

        public string PartnerAlias { get; set; }

        public string PartnerSessionId { get; set; }

        public string StartPointAlias { get; set; }
        
    }
}

