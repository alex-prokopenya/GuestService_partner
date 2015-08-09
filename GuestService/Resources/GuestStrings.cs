namespace GuestService.Resources
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    using Sm.System.Mvc.Language;
    using System.Collections.Generic;
    using System.Collections;

    [CompilerGenerated, DebuggerNonUserCode, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    public class GuestStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        private static Dictionary<string, Dictionary<string, string>> strings = new Dictionary<string, Dictionary<string, string>>();

        public static string Get(string key)
        {
            var str = UrlLanguage.CurrentLanguage;

            if (!strings.ContainsKey(str))
            {
                strings[str] = new Dictionary<string, string>();

                //загрузить строки из xml
                string fileName = System.IO.Path.Combine(GuestService.Notifications.TemplateParser.GetAssemblyDirectory(), "Resources", "GuestStrings." + str + ".resx");

                if (System.IO.File.Exists(fileName))
                {
                    ResXResourceReader rr = new ResXResourceReader(fileName);

                    foreach (DictionaryEntry d in rr)
                        strings[str].Add(d.Key.ToString(), d.Value.ToString());
                }
            }

            if (strings[str].ContainsKey(key))
                return strings[str][key];

            return GuestStrings.ResourceManager.GetString(key, GuestStrings.resourceCulture);
        }

        internal GuestStrings()
        {
        }

        public static string Authenticate_1
        {
            get
            {
                return ResourceManager.GetString("Authenticate_1", resourceCulture);
            }
        }

        public static string Authenticate_2
        {
            get
            {
                return ResourceManager.GetString("Authenticate_2", resourceCulture);
            }
        }

        public static string Authenticate_3
        {
            get
            {
                return ResourceManager.GetString("Authenticate_3", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        public static string DepartureBack
        {
            get
            {
                return ResourceManager.GetString("DepartureBack", resourceCulture);
            }
        }

        public static string DepartureNotFound
        {
            get
            {
                return ResourceManager.GetString("DepartureNotFound", resourceCulture);
            }
        }

        public static string DepartureNotFoundNote_1
        {
            get
            {
                return ResourceManager.GetString("DepartureNotFoundNote_1", resourceCulture);
            }
        }

        public static string DepartureNotFoundNote_2
        {
            get
            {
                return ResourceManager.GetString("DepartureNotFoundNote_2", resourceCulture);
            }
        }

        public static string DepartureNoTransferFound
        {
            get
            {
                return ResourceManager.GetString("DepartureNoTransferFound", resourceCulture);
            }
        }

        public static string DepartureTitle
        {
            get
            {
                return ResourceManager.GetString("DepartureTitle", resourceCulture);
            }
        }

        public static string FindOrderChoose_1
        {
            get
            {
                return ResourceManager.GetString("FindOrderChoose_1", resourceCulture);
            }
        }

        public static string FindOrderChoose_2
        {
            get
            {
                return ResourceManager.GetString("FindOrderChoose_2", resourceCulture);
            }
        }

        public static string FindOrderClaim
        {
            get
            {
                return ResourceManager.GetString("FindOrderClaim", resourceCulture);
            }
        }

        public static string FindOrderDeleteButton
        {
            get
            {
                return ResourceManager.GetString("FindOrderDeleteButton", resourceCulture);
            }
        }

        public static string FindOrderFindButton
        {
            get
            {
                return ResourceManager.GetString("FindOrderFindButton", resourceCulture);
            }
        }

        public static string FindOrderFound
        {
            get
            {
                return ResourceManager.GetString("FindOrderFound", resourceCulture);
            }
        }

        public static string FindOrderKnow_1
        {
            get
            {
                return ResourceManager.GetString("FindOrderKnow_1", resourceCulture);
            }
        }

        public static string FindOrderKnow_2
        {
            get
            {
                return ResourceManager.GetString("FindOrderKnow_2", resourceCulture);
            }
        }

        public static string FindOrderLinkOrderButton
        {
            get
            {
                return ResourceManager.GetString("FindOrderLinkOrderButton", resourceCulture);
            }
        }

        public static string FindOrderModel_D_Claim
        {
            get
            {
                return ResourceManager.GetString("FindOrderModel_D_Claim", resourceCulture);
            }
        }

        public static string FindOrderModel_N_Claim
        {
            get
            {
                return ResourceManager.GetString("FindOrderModel_N_Claim", resourceCulture);
            }
        }

        public static string FindOrderModel_N_ClaimName
        {
            get
            {
                return ResourceManager.GetString("FindOrderModel_N_ClaimName", resourceCulture);
            }
        }

        public static string FindOrderModel_N_Passport
        {
            get
            {
                return ResourceManager.GetString("FindOrderModel_N_Passport", resourceCulture);
            }
        }

        public static string FindOrderModel_R_Claim
        {
            get
            {
                return ResourceManager.GetString("FindOrderModel_R_Claim", resourceCulture);
            }
        }

        public static string FindOrderModel_R_ClaimName
        {
            get
            {
                return ResourceManager.GetString("FindOrderModel_R_ClaimName", resourceCulture);
            }
        }

        public static string FindOrderModel_R_Passport
        {
            get
            {
                return ResourceManager.GetString("FindOrderModel_R_Passport", resourceCulture);
            }
        }

        public static string FindOrderName
        {
            get
            {
                return ResourceManager.GetString("FindOrderName", resourceCulture);
            }
        }

        public static string FindOrderNameSmall
        {
            get
            {
                return ResourceManager.GetString("FindOrderNameSmall", resourceCulture);
            }
        }

        public static string FindOrderNoLinkedOrders
        {
            get
            {
                return ResourceManager.GetString("FindOrderNoLinkedOrders", resourceCulture);
            }
        }

        public static string FindOrderNotFound
        {
            get
            {
                return ResourceManager.GetString("FindOrderNotFound", resourceCulture);
            }
        }

        public static string FindOrderOrderTitle
        {
            get
            {
                return ResourceManager.GetString("FindOrderOrderTitle", resourceCulture);
            }
        }

        public static string FindOrderPassSer
        {
            get
            {
                return ResourceManager.GetString("FindOrderPassSer", resourceCulture);
            }
        }

        public static string FindOrderTitle
        {
            get
            {
                return ResourceManager.GetString("FindOrderTitle", resourceCulture);
            }
        }

        public static string GuestServicesExcursionDepartureTitle
        {
            get
            {
                return ResourceManager.GetString("GuestServicesExcursionDepartureTitle", resourceCulture);
            }
        }

        public static string GuestServicesFindOrder_1
        {
            get
            {
                return ResourceManager.GetString("GuestServicesFindOrder_1", resourceCulture);
            }
        }

        public static string GuestServicesFindOrder_2
        {
            get
            {
                return ResourceManager.GetString("GuestServicesFindOrder_2", resourceCulture);
            }
        }

        public static string GuestServicesFindOrder_3
        {
            get
            {
                return ResourceManager.GetString("GuestServicesFindOrder_3", resourceCulture);
            }
        }

        public static string GuestServicesLink
        {
            get
            {
                return ResourceManager.GetString("GuestServicesLink", resourceCulture);
            }
        }

        public static string GuestServicesTitle
        {
            get
            {
                return ResourceManager.GetString("GuestServicesTitle", resourceCulture);
            }
        }

        public static string GuestServiceTitle
        {
            get
            {
                return ResourceManager.GetString("GuestServiceTitle", resourceCulture);
            }
        }

        public static string HotelAlt
        {
            get
            {
                return ResourceManager.GetString("HotelAlt", resourceCulture);
            }
        }

        public static string HotelDepartureNoInformation
        {
            get
            {
                return ResourceManager.GetString("HotelDepartureNoInformation", resourceCulture);
            }
        }

        public static string HotelDepartureTitle
        {
            get
            {
                return ResourceManager.GetString("HotelDepartureTitle", resourceCulture);
            }
        }

        public static string HotelNotFound
        {
            get
            {
                return ResourceManager.GetString("HotelNotFound", resourceCulture);
            }
        }

        public static string HotelNotFound_1
        {
            get
            {
                return ResourceManager.GetString("HotelNotFound_1", resourceCulture);
            }
        }

        public static string HotelNotFound_Link
        {
            get
            {
                return ResourceManager.GetString("HotelNotFound_Link", resourceCulture);
            }
        }

        public static string HotelTitle
        {
            get
            {
                return ResourceManager.GetString("HotelTitle", resourceCulture);
            }
        }

        public static string MenuBookingAlt
        {
            get
            {
                return ResourceManager.GetString("MenuBookingAlt", resourceCulture);
            }
        }

        public static string MenuBookingText
        {
            get
            {
                return ResourceManager.GetString("MenuBookingText", resourceCulture);
            }
        }

        public static string MenuBookingTitle
        {
            get
            {
                return ResourceManager.GetString("MenuBookingTitle", resourceCulture);
            }
        }

        public static string MenuDepartureAlt
        {
            get
            {
                return ResourceManager.GetString("MenuDepartureAlt", resourceCulture);
            }
        }

        public static string MenuDepartureText
        {
            get
            {
                return ResourceManager.GetString("MenuDepartureText", resourceCulture);
            }
        }

        public static string MenuDepartureTitle
        {
            get
            {
                return ResourceManager.GetString("MenuDepartureTitle", resourceCulture);
            }
        }

        public static string MenuExcursionAlt
        {
            get
            {
                return ResourceManager.GetString("MenuExcursionAlt", resourceCulture);
            }
        }

        public static string MenuExcursionText
        {
            get
            {
                return ResourceManager.GetString("MenuExcursionText", resourceCulture);
            }
        }

        public static string MenuExcursionTitle
        {
            get
            {
                return ResourceManager.GetString("MenuExcursionTitle", resourceCulture);
            }
        }

        public static string MenuInformationAlt
        {
            get
            {
                return ResourceManager.GetString("MenuInformationAlt", resourceCulture);
            }
        }

        public static string MenuInformationText
        {
            get
            {
                return ResourceManager.GetString("MenuInformationText", resourceCulture);
            }
        }

        public static string MenuInformationTitle
        {
            get
            {
                return ResourceManager.GetString("MenuInformationTitle", resourceCulture);
            }
        }

        public static string MenuOrdersAlt
        {
            get
            {
                return ResourceManager.GetString("MenuOrdersAlt", resourceCulture);
            }
        }

        public static string MenuOrdersText
        {
            get
            {
                return ResourceManager.GetString("MenuOrdersText", resourceCulture);
            }
        }

        public static string MenuOrdersTitle
        {
            get
            {
                return ResourceManager.GetString("MenuOrdersTitle", resourceCulture);
            }
        }

        public static string MoreOrderAlt
        {
            get
            {
                return ResourceManager.GetString("MoreOrderAlt", resourceCulture);
            }
        }

        public static string MoreOrdersFindButton
        {
            get
            {
                return ResourceManager.GetString("MoreOrdersFindButton", resourceCulture);
            }
        }

        public static string MoreOrdersInstruction
        {
            get
            {
                return ResourceManager.GetString("MoreOrdersInstruction", resourceCulture);
            }
        }

        public static string MoreOrdersListOrderAlt
        {
            get
            {
                return ResourceManager.GetString("MoreOrdersListOrderAlt", resourceCulture);
            }
        }

        public static string MoreOrderTitle
        {
            get
            {
                return ResourceManager.GetString("MoreOrderTitle", resourceCulture);
            }
        }

        public static string OrderDoPaymentButton
        {
            get
            {
                return ResourceManager.GetString("OrderDoPaymentButton", resourceCulture);
            }
        }

        public static string OrderInfoTitle
        {
            get
            {
                return ResourceManager.GetString("OrderInfoTitle", resourceCulture);
            }
        }

        public static string OrderToPay
        {
            get
            {
                return ResourceManager.GetString("OrderToPay", resourceCulture);
            }
        }

        public static string OrderTotal
        {
            get
            {
                return ResourceManager.GetString("OrderTotal", resourceCulture);
            }
        }

        public static string PrintOrderBuildVoucherButton
        {
            get
            {
                return ResourceManager.GetString("PrintOrderBuildVoucherButton", resourceCulture);
            }
        }

        public static string PrintOrderConfirmCaption
        {
            get
            {
                return ResourceManager.GetString("PrintOrderConfirmCaption", resourceCulture);
            }
        }

        public static string PrintOrderModel_Claim
        {
            get
            {
                return ResourceManager.GetString("PrintOrderModel_Claim", resourceCulture);
            }
        }

        public static string PrintOrderModel_D_Claim
        {
            get
            {
                return ResourceManager.GetString("PrintOrderModel_D_Claim", resourceCulture);
            }
        }

        public static string PrintOrderModel_N_Claim
        {
            get
            {
                return ResourceManager.GetString("PrintOrderModel_N_Claim", resourceCulture);
            }
        }

        public static string PrintOrderModel_N_Name
        {
            get
            {
                return ResourceManager.GetString("PrintOrderModel_N_Name", resourceCulture);
            }
        }

        public static string PrintOrderModel_Name_1
        {
            get
            {
                return ResourceManager.GetString("PrintOrderModel_Name_1", resourceCulture);
            }
        }

        public static string PrintOrderModel_Name_2
        {
            get
            {
                return ResourceManager.GetString("PrintOrderModel_Name_2", resourceCulture);
            }
        }

        public static string PrintOrderModel_R_Claim
        {
            get
            {
                return ResourceManager.GetString("PrintOrderModel_R_Claim", resourceCulture);
            }
        }

        public static string PrintOrderModel_R_Name
        {
            get
            {
                return ResourceManager.GetString("PrintOrderModel_R_Name", resourceCulture);
            }
        }

        public static string PrintOrderNotFound
        {
            get
            {
                return ResourceManager.GetString("PrintOrderNotFound", resourceCulture);
            }
        }

        public static string PrintOrderTitle
        {
            get
            {
                return ResourceManager.GetString("PrintOrderTitle", resourceCulture);
            }
        }

        public static string PrintVoucherButton
        {
            get
            {
                return ResourceManager.GetString("PrintVoucherButton", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("GuestService.Resources.GuestStrings", typeof(GuestStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        public static string String1
        {
            get
            {
                return ResourceManager.GetString("String1", resourceCulture);
            }
        }

        public static string SummaryCheckAnother
        {
            get
            {
                return ResourceManager.GetString("SummaryCheckAnother", resourceCulture);
            }
        }

        public static string SummaryFindButton
        {
            get
            {
                return ResourceManager.GetString("SummaryFindButton", resourceCulture);
            }
        }

        public static string SummaryFindReservationTitle
        {
            get
            {
                return ResourceManager.GetString("SummaryFindReservationTitle", resourceCulture);
            }
        }

        public static string SummaryNameLabel
        {
            get
            {
                return ResourceManager.GetString("SummaryNameLabel", resourceCulture);
            }
        }

        public static string SummaryNameLabel_1
        {
            get
            {
                return ResourceManager.GetString("SummaryNameLabel_1", resourceCulture);
            }
        }

        public static string SummaryNotFound
        {
            get
            {
                return ResourceManager.GetString("SummaryNotFound", resourceCulture);
            }
        }

        public static string SummaryReservationLabel
        {
            get
            {
                return ResourceManager.GetString("SummaryReservationLabel", resourceCulture);
            }
        }
    }
}

