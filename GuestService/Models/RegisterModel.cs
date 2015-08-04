namespace GuestService.Models
{
    using GuestService.Resources;
    using Sm.System.Mvc.Validation;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class RegisterModel
    {
        [DataType(DataType.Password), System.Web.Mvc.Compare("Password", ErrorMessageResourceName="LoginModel_C_ConfirmPassword", ErrorMessageResourceType=typeof(AccountStrings))]
        public string ConfirmPassword { get; set; }

        [Required, StringLength(100, ErrorMessageResourceName="LoginModel_L_Password", ErrorMessageResourceType=typeof(AccountStrings), MinimumLength=6), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, Email(ErrorMessageResourceName="LoginModel_R_Mail", ErrorMessageResourceType=typeof(AccountStrings))]
        public string UserName { get; set; }
    }
}

