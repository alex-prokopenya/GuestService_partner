namespace GuestService.Models.Guest
{
    using System;
    using System.Runtime.CompilerServices;

    public class GuestWebParams
    {
        public DateTime? _dt { get; set; }

        public DateTime? TestDate
        {
            get
            {
                return this._dt;
            }
        }
    }
}

