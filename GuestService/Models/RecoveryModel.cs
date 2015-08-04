namespace GuestService.Models
{
    using GuestService.Resources;
    using Sm.System.Mvc.Validation;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class RecoveryModel
    {
        public string ResetToken { get; set; }

        [Required(ErrorMessageResourceName="RecoveryModel_R_UserName", ErrorMessageResourceType=typeof(AccountStrings)), Email(ErrorMessageResourceName="LoginModel_R_Mail", ErrorMessageResourceType=typeof(AccountStrings))]
        public string UserName { get; set; }
    }
}

