namespace GuestService.Models.Guest
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class UnlinkOrderModel
    {
        [Required]
        public int? Claim { get; set; }
    }
}

