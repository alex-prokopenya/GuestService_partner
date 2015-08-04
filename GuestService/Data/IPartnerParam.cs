namespace GuestService.Data
{
    using System;

    public interface IPartnerParam
    {
        string pa { get; set; }

        string PartnerAlias { get; }

        string PartnerSessionID { get; }

        string psid { get; set; }
    }
}

