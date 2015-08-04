namespace GuestService.Models.Guest
{
    using GuestService.Resources;
    using Sm.System.Mvc.Validation;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class FindOrderModel
    {
        [Required(ErrorMessageResourceName="FindOrderModel_R_Claim", ErrorMessageResourceType=typeof(GuestStrings)), Display(Name="FindOrderModel_N_Claim", ResourceType=typeof(GuestStrings)), Digital(ErrorMessageResourceName="FindOrderModel_D_Claim", ErrorMessageResourceType=typeof(GuestStrings))]
        public string Claim { get; set; }

        [Required(ErrorMessageResourceName="FindOrderModel_R_ClaimName", ErrorMessageResourceType=typeof(GuestStrings)), Display(Name="FindOrderModel_N_ClaimName", ResourceType=typeof(GuestStrings))]
        public string ClaimName { get; set; }

        [Display(Name="FindOrderModel_N_Passport", ResourceType=typeof(GuestStrings)), Required(ErrorMessageResourceName="FindOrderModel_R_Passport", ErrorMessageResourceType=typeof(GuestStrings))]
        public string Passport { get; set; }

        [Display(Name="FindOrderModel_N_ClaimName", ResourceType=typeof(GuestStrings)), Required(ErrorMessageResourceName="FindOrderModel_R_ClaimName", ErrorMessageResourceType=typeof(GuestStrings))]
        public string PassportName { get; set; }

        public string RequestType { get; set; }
    }
}

