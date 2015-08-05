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

    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode]
    public class SharedStrings
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal SharedStrings()
        {
        }

        private static Dictionary<string, Dictionary<string, string>> strings = new Dictionary<string, Dictionary<string, string>>();

        public static string Get(string key)
        {
            var str = UrlLanguage.CurrentLanguage;

            if (strings.Count == 0)
            {
                strings[str] = new Dictionary<string, string>();

                //загрузить строки из xml
                string fileName = System.IO.Path.Combine(GuestService.Notifications.TemplateParser.GetAssemblyDirectory(), "Resources", "SharedStrings." + str + ".resx");

                if (System.IO.File.Exists(fileName))
                {
                    ResXResourceReader rr = new ResXResourceReader(fileName);

                    foreach (DictionaryEntry d in rr)
                        strings[str].Add(d.Key.ToString(), d.Value.ToString());
                }
            }

            if (strings[str].ContainsKey(key))
                return strings[str][key];

            return SharedStrings.ResourceManager.GetString(key, SharedStrings.resourceCulture);
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

        public static string DepartureHotel_Flight
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_Flight", resourceCulture);
            }
        }

        public static string DepartureHotel_FlightDestination
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_FlightDestination", resourceCulture);
            }
        }

        public static string DepartureHotel_FlightSource
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_FlightSource", resourceCulture);
            }
        }

        public static string DepartureHotel_GuestList
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_GuestList", resourceCulture);
            }
        }

        public static string DepartureHotel_Guide
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_Guide", resourceCulture);
            }
        }

        public static string DepartureHotel_Guide2
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_Guide2", resourceCulture);
            }
        }

        public static string DepartureHotel_GuidePhone
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_GuidePhone", resourceCulture);
            }
        }

        public static string DepartureHotel_Hotel
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_Hotel", resourceCulture);
            }
        }

        public static string DepartureHotel_HotelRegion
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_HotelRegion", resourceCulture);
            }
        }

        public static string DepartureHotel_HotelTown
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_HotelTown", resourceCulture);
            }
        }

        public static string DepartureHotel_TransferBus
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_TransferBus", resourceCulture);
            }
        }

        public static string DepartureHotel_TransferDate
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_TransferDate", resourceCulture);
            }
        }

        public static string DepartureHotel_TransferIndividual
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_TransferIndividual", resourceCulture);
            }
        }

        public static string DepartureHotel_TransferNote
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_TransferNote", resourceCulture);
            }
        }

        public static string DepartureHotel_TransferTime
        {
            get
            {
                return ResourceManager.GetString("DepartureHotel_TransferTime", resourceCulture);
            }
        }

        public static string Error_MainPageLink
        {
            get
            {
                return ResourceManager.GetString("Error_MainPageLink", resourceCulture);
            }
        }

        public static string Error_Text
        {
            get
            {
                return ResourceManager.GetString("Error_Text", resourceCulture);
            }
        }

        public static string Error_Title
        {
            get
            {
                return ResourceManager.GetString("Error_Title", resourceCulture);
            }
        }

        public static string Error404_Departure
        {
            get
            {
                return ResourceManager.GetString("Error404_Departure", resourceCulture);
            }
        }

        public static string Error404_Excursion
        {
            get
            {
                return ResourceManager.GetString("Error404_Excursion", resourceCulture);
            }
        }

        public static string Error404_GuestService
        {
            get
            {
                return ResourceManager.GetString("Error404_GuestService", resourceCulture);
            }
        }

        public static string Error404_Reservation
        {
            get
            {
                return ResourceManager.GetString("Error404_Reservation", resourceCulture);
            }
        }

        public static string Error404_Text
        {
            get
            {
                return ResourceManager.GetString("Error404_Text", resourceCulture);
            }
        }

        public static string Error404_Title
        {
            get
            {
                return ResourceManager.GetString("Error404_Title", resourceCulture);
            }
        }

        public static string ErrorSummary_Title
        {
            get
            {
                return ResourceManager.GetString("ErrorSummary_Title", resourceCulture);
            }
        }

        public static string ExcursionTransfer_Date
        {
            get
            {
                return ResourceManager.GetString("ExcursionTransfer_Date", resourceCulture);
            }
        }

        public static string ExcursionTransfer_Guide2Name
        {
            get
            {
                return ResourceManager.GetString("ExcursionTransfer_Guide2Name", resourceCulture);
            }
        }

        public static string ExcursionTransfer_Guide2Phone
        {
            get
            {
                return ResourceManager.GetString("ExcursionTransfer_Guide2Phone", resourceCulture);
            }
        }

        public static string ExcursionTransfer_GuideName
        {
            get
            {
                return ResourceManager.GetString("ExcursionTransfer_GuideName", resourceCulture);
            }
        }

        public static string ExcursionTransfer_GuidePhone
        {
            get
            {
                return ResourceManager.GetString("ExcursionTransfer_GuidePhone", resourceCulture);
            }
        }

        public static string ExcursionTransfer_PickupPlace
        {
            get
            {
                return ResourceManager.GetString("ExcursionTransfer_PickupPlace", resourceCulture);
            }
        }

        public static string ExcursionTransfer_PickupTime
        {
            get
            {
                return ResourceManager.GetString("ExcursionTransfer_PickupTime", resourceCulture);
            }
        }

        public static string HotelGuide_HotelDuty
        {
            get
            {
                return ResourceManager.GetString("HotelGuide_HotelDuty", resourceCulture);
            }
        }

        public static string HotelGuide_Phone
        {
            get
            {
                return ResourceManager.GetString("HotelGuide_Phone", resourceCulture);
            }
        }

        public static string NavigatorLogout
        {
            get
            {
                return ResourceManager.GetString("NavigatorLogout", resourceCulture);
            }
        }

        public static string NavigatorMyOrders
        {
            get
            {
                return ResourceManager.GetString("NavigatorMyOrders", resourceCulture);
            }
        }

        public static string NavigatorTitle
        {
            get
            {
                return ResourceManager.GetString("NavigatorTitle", resourceCulture);
            }
        }

        public static string ReservationOrder_Adult
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_Adult", resourceCulture);
            }
        }

        public static string ReservationOrder_Child
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_Child", resourceCulture);
            }
        }

        public static string ReservationOrder_ExcursionAlt
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_ExcursionAlt", resourceCulture);
            }
        }

        public static string ReservationOrder_ExcursionLang
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_ExcursionLang", resourceCulture);
            }
        }

        public static string ReservationOrder_ExcursionTime
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_ExcursionTime", resourceCulture);
            }
        }

        public static string ReservationOrder_FreightAlt
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_FreightAlt", resourceCulture);
            }
        }

        public static string ReservationOrder_FreightArrival
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_FreightArrival", resourceCulture);
            }
        }

        public static string ReservationOrder_FreightClass
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_FreightClass", resourceCulture);
            }
        }

        public static string ReservationOrder_FreightDeparture
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_FreightDeparture", resourceCulture);
            }
        }

        public static string ReservationOrder_FreightSeat
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_FreightSeat", resourceCulture);
            }
        }

        public static string ReservationOrder_HotelAlt
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_HotelAlt", resourceCulture);
            }
        }

        public static string ReservationOrder_Infant
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_Infant", resourceCulture);
            }
        }

        public static string ReservationOrder_Meal
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_Meal", resourceCulture);
            }
        }

        public static string ReservationOrder_Note
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_Note", resourceCulture);
            }
        }

        public static string ReservationOrder_PickupHotel
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_PickupHotel", resourceCulture);
            }
        }

        public static string ReservationOrder_PickupPoint
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_PickupPoint", resourceCulture);
            }
        }

        public static string ReservationOrder_PriceFree
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_PriceFree", resourceCulture);
            }
        }

        public static string ReservationOrder_Room
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_Room", resourceCulture);
            }
        }

        public static string ReservationOrder_ServiceAlt
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_ServiceAlt", resourceCulture);
            }
        }

        public static string ReservationOrder_ServiceType
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_ServiceType", resourceCulture);
            }
        }

        public static string ReservationOrder_TransferAlt
        {
            get
            {
                return ResourceManager.GetString("ReservationOrder_TransferAlt", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager manager = new System.Resources.ResourceManager("GuestService.Resources.SharedStrings", typeof(SharedStrings).Assembly);
                    resourceMan = manager;
                }
                return resourceMan;
            }
        }

        public static string ToolbarGuestOrderLink
        {
            get
            {
                return ResourceManager.GetString("ToolbarGuestOrderLink", resourceCulture);
            }
        }

        public static string ToolbarSignIn
        {
            get
            {
                return ResourceManager.GetString("ToolbarSignIn", resourceCulture);
            }
        }

        public static string ValidationSummary_Title
        {
            get
            {
                return ResourceManager.GetString("ValidationSummary_Title", resourceCulture);
            }
        }
    }
}

