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
    public class BookingStrings
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
                string fileName = System.IO.Path.Combine(GuestService.Notifications.TemplateParser.GetAssemblyDirectory(), "Resources", "BookingStrings." + str + ".resx");

                if (System.IO.File.Exists(fileName))
                {
                    ResXResourceReader rr = new ResXResourceReader(fileName);

                    foreach (DictionaryEntry d in rr)
                        strings[str].Add(d.Key.ToString(), d.Value.ToString());
                }
            }

            if (strings[str].ContainsKey(key))
                return strings[str][key];

            return BookingStrings.ResourceManager.GetString(key, BookingStrings.resourceCulture);
        }

        internal BookingStrings()
        {
        }

        public static string BookingAgreementCancel
        {
            get
            {
                return ResourceManager.GetString("BookingAgreementCancel", resourceCulture);
            }
        }

        public static string BookingAgreementConfirm
        {
            get
            {
                return ResourceManager.GetString("BookingAgreementConfirm", resourceCulture);
            }
        }

        public static string BookingAgreementConfirmMessage_1
        {
            get
            {
                return ResourceManager.GetString("BookingAgreementConfirmMessage_1", resourceCulture);
            }
        }

        public static string BookingAgreementConfirmMessage_2
        {
            get
            {
                return ResourceManager.GetString("BookingAgreementConfirmMessage_2", resourceCulture);
            }
        }

        public static string BookingAgreementTitle
        {
            get
            {
                return ResourceManager.GetString("BookingAgreementTitle", resourceCulture);
            }
        }

        public static string BookingErrorTitle
        {
            get
            {
                return ResourceManager.GetString("BookingErrorTitle", resourceCulture);
            }
        }

        public static string BookingFormAddress
        {
            get
            {
                return ResourceManager.GetString("BookingFormAddress", resourceCulture);
            }
        }

        public static string BookingFormMail
        {
            get
            {
                return ResourceManager.GetString("BookingFormMail", resourceCulture);
            }
        }

        public static string BookingFormName
        {
            get
            {
                return ResourceManager.GetString("BookingFormName", resourceCulture);
            }
        }

        public static string BookingFormNote
        {
            get
            {
                return ResourceManager.GetString("BookingFormNote", resourceCulture);
            }
        }

        public static string BookingFormPhone
        {
            get
            {
                return ResourceManager.GetString("BookingFormPhone", resourceCulture);
            }
        }

        public static string BookingInProcess
        {
            get
            {
                return ResourceManager.GetString("BookingInProcess", resourceCulture);
            }
        }

        public static string BookingMenuTitle
        {
            get
            {
                return ResourceManager.GetString("BookingMenuTitle", resourceCulture);
            }
        }

        public static string BookingModel_R_CustomerMobile
        {
            get
            {
                return ResourceManager.GetString("BookingModel_R_CustomerMobile", resourceCulture);
            }
        }

        public static string BookingModel_R_CustomerName
        {
            get
            {
                return ResourceManager.GetString("BookingModel_R_CustomerName", resourceCulture);
            }
        }

        public static string BookingModel_R_UserEmail
        {
            get
            {
                return ResourceManager.GetString("BookingModel_R_UserEmail", resourceCulture);
            }
        }

        public static string BookingPayButton
        {
            get
            {
                return ResourceManager.GetString("BookingPayButton", resourceCulture);
            }
        }

        public static string BookingProcessing_OrderTitle_1
        {
            get
            {
                return ResourceManager.GetString("BookingProcessing_OrderTitle_1", resourceCulture);
            }
        }

        public static string BookingProcessing_OrderTitle_2
        {
            get
            {
                return ResourceManager.GetString("BookingProcessing_OrderTitle_2", resourceCulture);
            }
        }

        public static string BookingProcessing_Title
        {
            get
            {
                return ResourceManager.GetString("BookingProcessing_Title", resourceCulture);
            }
        }

        public static string BookingResultText_1
        {
            get
            {
                return ResourceManager.GetString("BookingResultText_1", resourceCulture);
            }
        }

        public static string BookingResultText_2
        {
            get
            {
                return ResourceManager.GetString("BookingResultText_2", resourceCulture);
            }
        }

        public static string BookingResultTitle
        {
            get
            {
                return ResourceManager.GetString("BookingResultTitle", resourceCulture);
            }
        }

        public static string BookingReturnToOrder
        {
            get
            {
                return ResourceManager.GetString("BookingReturnToOrder", resourceCulture);
            }
        }

        public static string BookNowButton
        {
            get
            {
                return ResourceManager.GetString("BookNowButton", resourceCulture);
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

        public static string EmptyCart_1
        {
            get
            {
                return ResourceManager.GetString("EmptyCart_1", resourceCulture);
            }
        }

        public static string EmptyCart_2
        {
            get
            {
                return ResourceManager.GetString("EmptyCart_2", resourceCulture);
            }
        }

        public static string ErrorMessageTitle
        {
            get
            {
                return ResourceManager.GetString("ErrorMessageTitle", resourceCulture);
            }
        }

        public static string GoBack
        {
            get
            {
                return ResourceManager.GetString("GoBack", resourceCulture);
            }
        }

        public static string GuestServiceTitle
        {
            get
            {
                return ResourceManager.GetString("GuestServiceTitle", resourceCulture);
            }
        }

        public static string OrderTotalLabel
        {
            get
            {
                return ResourceManager.GetString("OrderTotalLabel", resourceCulture);
            }
        }

        public static string Promo_ApplyCommand
        {
            get
            {
                return ResourceManager.GetString("Promo_ApplyCommand", resourceCulture);
            }
        }

        public static string Promo_ApplyLabel
        {
            get
            {
                return ResourceManager.GetString("Promo_ApplyLabel", resourceCulture);
            }
        }

        public static string Promo_FormButton
        {
            get
            {
                return ResourceManager.GetString("Promo_FormButton", resourceCulture);
            }
        }

        public static string Promo_FormLabel
        {
            get
            {
                return ResourceManager.GetString("Promo_FormLabel", resourceCulture);
            }
        }

        public static string Promo_FormTitle
        {
            get
            {
                return ResourceManager.GetString("Promo_FormTitle", resourceCulture);
            }
        }

        public static string RemoveOrderCancelButton
        {
            get
            {
                return ResourceManager.GetString("RemoveOrderCancelButton", resourceCulture);
            }
        }

        public static string RemoveOrderConfirmButton
        {
            get
            {
                return ResourceManager.GetString("RemoveOrderConfirmButton", resourceCulture);
            }
        }

        public static string RemoveOrderConfirmText
        {
            get
            {
                return ResourceManager.GetString("RemoveOrderConfirmText", resourceCulture);
            }
        }

        public static string RemoveOrderConfirmTitle
        {
            get
            {
                return ResourceManager.GetString("RemoveOrderConfirmTitle", resourceCulture);
            }
        }

        public static string RemoveOrderLink
        {
            get
            {
                return ResourceManager.GetString("RemoveOrderLink", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("GuestService.Resources.BookingStrings", typeof(BookingStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        public static string RulesAccepted
        {
            get
            {
                return ResourceManager.GetString("RulesAccepted", resourceCulture);
            }
        }

        public static string Title
        {
            get
            {
                return ResourceManager.GetString("Title", resourceCulture);
            }
        }
    }
}

