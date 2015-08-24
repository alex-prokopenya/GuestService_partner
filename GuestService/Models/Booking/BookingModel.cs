namespace GuestService.Models.Booking
{
    using GuestService.Resources;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class BookingModel
    {
        public BookingModel()
        {
            this.Orders = new List<BookingOrderModel>();
        }

        public string Action { get; set; }

        public string BookingNote { get; set; }

        public string CustomerAddress { get; set; }

        [Required(ErrorMessageResourceName="BookingModel_R_UserEmail", ErrorMessageResourceType=typeof(BookingStrings))]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessageResourceName="BookingModel_R_CustomerMobile", ErrorMessageResourceType=typeof(BookingStrings))]
        public string CustomerMobile { get; set; }

        [Required(ErrorMessageResourceName="BookingModel_R_CustomerName", ErrorMessageResourceType=typeof(BookingStrings))]
        public string CustomerName { get; set; }

        public List<BookingOrderModel> Orders { get; private set; }

        public string PartnerAlias { get; set; }

        public string PartnerSessionID { get; set; }

        public string PromoCode { get; set; }

        public string[] PromoCodes { get; set; }

        public string RemoveOrderId { get; set; }

        public string PaymentMethod { get; set; }

        public bool RulesAccepted { get; set; }
    }
}

