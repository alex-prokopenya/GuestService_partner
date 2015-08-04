namespace GuestService.Models
{
    using GuestService.Resources;
    using Sm.System.Mvc.Validation;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class RegisterExternalLoginModel
    {
        public string ExternalLoginData { get; set; }

        [Email(ErrorMessageResourceName="LoginModel_R_Mail", ErrorMessageResourceType=typeof(AccountStrings)), Required]
        public string UserName { get; set; }
    }
}

