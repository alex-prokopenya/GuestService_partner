namespace GuestService.Models
{
    using GuestService.Resources;
    using Sm.System.Mvc.Validation;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class LoginModel
    {
        [DataType(DataType.Password), Required(ErrorMessageResourceName="LoginModel_R_Password", ErrorMessageResourceType=typeof(AccountStrings))]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        [Email(ErrorMessageResourceName="LoginModel_R_Mail", ErrorMessageResourceType=typeof(AccountStrings)), Required(ErrorMessageResourceName="LoginModel_R_UserName", ErrorMessageResourceType=typeof(AccountStrings))]
        public string UserName { get; set; }
    }
}

