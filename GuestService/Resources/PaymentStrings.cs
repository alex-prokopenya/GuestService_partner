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
    public class PaymentStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        private static Dictionary<string, Dictionary<string, string>> strings = new Dictionary<string, Dictionary<string, string>>();

        public static string Get(string key)
        {
            var str = UrlLanguage.CurrentLanguage;

            if (strings.Count == 0)
            {
                strings[str] = new Dictionary<string, string>();

                //загрузить строки из xml
                string fileName = System.IO.Path.Combine(GuestService.Notifications.TemplateParser.GetAssemblyDirectory(), "Resources", "PaymentStrings." + str + ".resx");

                if (System.IO.File.Exists(fileName))
                {
                    ResXResourceReader rr = new ResXResourceReader(fileName);

                    foreach (DictionaryEntry d in rr)
                        strings[str].Add(d.Key.ToString(), d.Value.ToString());
                }
            }

            if (strings[str].ContainsKey(key))
                return strings[str][key];

            return PaymentStrings.ResourceManager.GetString(key, PaymentStrings.resourceCulture);
        }

        internal PaymentStrings()
        {
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

        public static string PaymentAfterConfirmationMessage
        {
            get
            {
                return ResourceManager.GetString("PaymentAfterConfirmationMessage", resourceCulture);
            }
        }

        public static string PaymentAlreadyPaid
        {
            get
            {
                return ResourceManager.GetString("PaymentAlreadyPaid", resourceCulture);
            }
        }

        public static string PaymentBankBill
        {
            get
            {
                return ResourceManager.GetString("PaymentBankBill", resourceCulture);
            }
        }

        public static string PaymentCancelled
        {
            get
            {
                return ResourceManager.GetString("PaymentCancelled", resourceCulture);
            }
        }

        public static string PaymentCannotPayOrder
        {
            get
            {
                return ResourceManager.GetString("PaymentCannotPayOrder", resourceCulture);
            }
        }

        public static string PaymentComission
        {
            get
            {
                return ResourceManager.GetString("PaymentComission", resourceCulture);
            }
        }

        public static string PaymentForOrderFormat
        {
            get
            {
                return ResourceManager.GetString("PaymentForOrderFormat", resourceCulture);
            }
        }

        public static string PaymentGuestService
        {
            get
            {
                return ResourceManager.GetString("PaymentGuestService", resourceCulture);
            }
        }

        public static string PaymentListTitle
        {
            get
            {
                return ResourceManager.GetString("PaymentListTitle", resourceCulture);
            }
        }

        public static string PaymentMainPageLink
        {
            get
            {
                return ResourceManager.GetString("PaymentMainPageLink", resourceCulture);
            }
        }

        public static string PaymentMethod
        {
            get
            {
                return ResourceManager.GetString("PaymentMethod", resourceCulture);
            }
        }

        public static string PaymentOrderTitle
        {
            get
            {
                return ResourceManager.GetString("PaymentOrderTitle", resourceCulture);
            }
        }

        public static string PaymentPayButton
        {
            get
            {
                return ResourceManager.GetString("PaymentPayButton", resourceCulture);
            }
        }

        public static string PaymentRedirectUniteller
        {
            get
            {
                return ResourceManager.GetString("PaymentRedirectUniteller", resourceCulture);
            }
        }

        public static string PaymentReservationCannotPay
        {
            get
            {
                return ResourceManager.GetString("PaymentReservationCannotPay", resourceCulture);
            }
        }

        public static string PaymentReservationNotFound
        {
            get
            {
                return ResourceManager.GetString("PaymentReservationNotFound", resourceCulture);
            }
        }

        public static string PaymentReservationStatusTitle
        {
            get
            {
                return ResourceManager.GetString("PaymentReservationStatusTitle", resourceCulture);
            }
        }

        public static string PaymentResultError
        {
            get
            {
                return ResourceManager.GetString("PaymentResultError", resourceCulture);
            }
        }

        public static string PaymentResultOK
        {
            get
            {
                return ResourceManager.GetString("PaymentResultOK", resourceCulture);
            }
        }

        public static string PaymentTitle
        {
            get
            {
                return ResourceManager.GetString("PaymentTitle", resourceCulture);
            }
        }

        public static string PaymentToPay
        {
            get
            {
                return ResourceManager.GetString("PaymentToPay", resourceCulture);
            }
        }

        public static string PaymentTotal
        {
            get
            {
                return ResourceManager.GetString("PaymentTotal", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("GuestService.Resources.PaymentStrings", typeof(PaymentStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }
    }
}

