namespace GuestService.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.CompilerServices;

    [System.ComponentModel.DataAnnotations.Schema.Table("guestservice_UserProfile")]
    public class UserProfile
    {
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity), Key]
        public int UserId { get; set; }

        public string UserName { get; set; }
    }
}

