namespace GuestService.Models.Excursion
{
    using GuestService.Data;
    using GuestService.Models;
    using System;
    using System.Runtime.CompilerServices;

    public class DatesParam : BaseApiParam, IPartnerParam
    {
        public DateTime? fd { get; set; }

        public DateTime? FirstDate
        {
            get
            {
                return this.fd;
            }
        }

        public DateTime? LastDate
        {
            get
            {
                return this.ld;
            }
        }

        public DateTime? ld { get; set; }

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

