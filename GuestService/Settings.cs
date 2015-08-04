namespace GuestService
{
    using Sm.System.SettingsExtension;
    using System;
    using System.Configuration;

    public static class Settings
    {
        public static int CartinfoPaxSelectMaxAdult
        {
            get
            {
                return ConfigurationManager.AppSettings.AsInt("cartinfoPaxSelectMaxAdult", 10);
            }
        }

        public static int CartinfoPaxSelectMaxChild
        {
            get
            {
                return ConfigurationManager.AppSettings.AsInt("cartinfoPaxSelectMaxChild", 10);
            }
        }

        public static int CartinfoPaxSelectMaxInfant
        {
            get
            {
                return ConfigurationManager.AppSettings.AsInt("cartinfoPaxSelectMaxInfant", 10);
            }
        }

        public static int ExcursionCheckAvailabilityDays
        {
            get
            {
                return ConfigurationManager.AppSettings.AsInt("excursionCheckAvailabilityDays", 100);
            }
        }

        public static int ExcursionDefaultDate
        {
            get
            {
                return ConfigurationManager.AppSettings.AsInt("excursionDefaultDate", 0);
            }
        }

        public static string ExcursionDefaultPartnerAlias
        {
            get
            {
                return ConfigurationManager.AppSettings.AsString("excursionDefaultPartnerAlias", null);
            }
        }

        public static int ExcursionGeographySearchLimit
        {
            get
            {
                return ConfigurationManager.AppSettings.AsInt("excursionGeographySearchLimit", 50);
            }
        }

        public static bool ExcursionWithPriceOnlyCatalog
        {
            get
            {
                return ConfigurationManager.AppSettings.AsBool("excursionWithPriceOnlyCatalog", false);
            }
        }

        public static string GuestDefaultPage
        {
            get
            {
                return ConfigurationManager.AppSettings.AsString("guestDefaultPage", "index");
            }
        }

        public static bool IsAddRankingEnabled
        {
            get
            {
                return ConfigurationManager.AppSettings.AsBool("isAddRankingEnabled", false);
            }
        }

        public static bool IsCacheDisabled
        {
            get
            {
                return ConfigurationManager.AppSettings.AsBool("isCacheDisabled", false);
            }
        }

        public static bool IsHideDeparturePoints
        {
            get
            {
                return ConfigurationManager.AppSettings.AsBool("isHideDeparturePoints", false);
            }
        }

        public static bool IsShowBreadCrumb
        {
            get
            {
                return ConfigurationManager.AppSettings.AsBool("isShowBreadCrumb", true);
            }
        }

        public static bool IsShowHotelGuideInfo
        {
            get
            {
                return ConfigurationManager.AppSettings.AsBool("isShowHotelGuideInfo", true);
            }
        }

        public static bool IsShowPromoCodeSection
        {
            get
            {
                return ConfigurationManager.AppSettings.AsBool("isShowPromoCodeSection", false);
            }
        }

        public static PaymentPaypalSettings PaymentPaypal
        {
            get
            {
                PaymentPaypalSettings settings = new PaymentPaypalSettings {
                    Username = ConfigurationManager.AppSettings.AsString("paymentPaypalUsername", null),
                    Password = ConfigurationManager.AppSettings.AsString("paymentPaypalPassword", null),
                    Signature = ConfigurationManager.AppSettings.AsString("paymentPaypalSinature", null),
                    IsSandbox = ConfigurationManager.AppSettings.AsBool("paymentPaypalSandbox", false)
                };
                return (((!string.IsNullOrEmpty(settings.Username) && !string.IsNullOrEmpty(settings.Password)) && !string.IsNullOrEmpty(settings.Signature)) ? settings : null);
            }
        }

        public static string ServiceBookingUrl
        {
            get
            {
                string str = ConfigurationManager.AppSettings.AsString("serviceBookingUrl", null);
                return (string.IsNullOrWhiteSpace(str) ? null : str);
            }
        }

        public static string ServiceHomeUrl
        {
            get
            {
                string str = ConfigurationManager.AppSettings.AsString("serviceHomeUrl", null);
                return (string.IsNullOrWhiteSpace(str) ? null : str);
            }
        }
    }
}

