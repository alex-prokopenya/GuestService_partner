namespace GuestService.Models
{
    using GuestService.Resources;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;
    using System.Web.Mvc;

    public class LocalPasswordModel
    {
        [DataType(DataType.Password), System.Web.Mvc.Compare("NewPassword", ErrorMessageResourceName="LocalPasswordModel_C_ConfirmPassword", ErrorMessageResourceType=typeof(AccountStrings))]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.Password), Required, StringLength(100, ErrorMessageResourceName="LocalPasswordModel_L_Password", ErrorMessageResourceType=typeof(AccountStrings), MinimumLength=6)]
        public string NewPassword { get; set; }

        [Required, DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}

