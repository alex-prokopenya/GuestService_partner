namespace GuestService.Models
{
    using GuestService.Resources;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class ResetPasswordModel
    {
        [System.Web.Mvc.Compare("Password", ErrorMessageResourceName="LocalPasswordModel_C_ConfirmPassword", ErrorMessageResourceType=typeof(AccountStrings)), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password), Required, StringLength(100, ErrorMessageResourceName="LocalPasswordModel_L_Password", ErrorMessageResourceType=typeof(AccountStrings), MinimumLength=6)]
        public string Password { get; set; }

        public string Token { get; set; }
    }
}

