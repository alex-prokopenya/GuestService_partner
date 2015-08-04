namespace GuestService.Models.Excursion
{
    using GuestService.Data;
    using System;
    using System.Runtime.CompilerServices;

    public class DestinationAndCategoryParam : CategoryParam, IPartnerParam
    {
        public string pa { get; set; }

        public string PartnerAlias
        {
            get
            {
                return this.pa;
            }
        }

        public string PartnerSessionID
        {
            get
            {
                return this.psid;
            }
        }

        public string psid { get; set; }
    }
}

