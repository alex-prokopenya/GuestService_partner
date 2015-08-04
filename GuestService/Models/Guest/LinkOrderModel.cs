namespace GuestService.Models.Guest
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LinkOrderModel
    {
        [Required]
        public int? Claim { get; set; }

        [Required]
        public string Name { get; set; }
    }
}

