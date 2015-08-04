namespace GuestService.Models.Guest
{
    using GuestService.Resources;
    using Sm.System.Mvc.Validation;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class PrintOrderModel
    {
        [Digital(ErrorMessageResourceName="PrintOrderModel_D_Claim", ErrorMessageResourceType=typeof(GuestStrings)), Display(Name="PrintOrderModel_N_Claim", ResourceType=typeof(GuestStrings)), Required(ErrorMessageResourceName="PrintOrderModel_R_Claim", ErrorMessageResourceType=typeof(GuestStrings))]
        public string Claim { get; set; }

        [Display(Name="PrintOrderModel_N_Name", ResourceType=typeof(GuestStrings)), Required(ErrorMessageResourceName="PrintOrderModel_R_Name", ErrorMessageResourceType=typeof(GuestStrings))]
        public string Name { get; set; }
    }
}

