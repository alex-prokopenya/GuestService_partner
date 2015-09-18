namespace GuestService.Controllers.Html
{
    using GuestService;
    using GuestService.Code;
    using GuestService.Models.Countries;
    using Sm.System.Mvc;
    using Sm.System.Mvc.Language;
    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using Sm.System.Database;
    using System.Data;

    [HttpPreferences, WebSecurityInitializer, UrlLanguage]
    public class CountriesController : BaseController
    {
        //по слагу определяем страну
        public static int? GetCountryBySlug(string slug)
        {
            var selectQuery = "select name, lname, inc from state where inc in (select state from region where inc in (select region from excurs))";

            //текущий язык
            DataSet set = DatabaseOperationProvider.Query(selectQuery, "regions", new { });

            var countries = new List<KeyValuePair<string, string>>();

            foreach (DataRow row in set.Tables["regions"].Rows)
            {
                var dbslug = StringsHelper.GenerateSlug(row["lname"].ToString());

                if (dbslug == slug)
                    return row.ReadInt("inc");
            }

            return null;
        }

        //по слагу определяем регион
        public static int? GetRegionBySlug(string slug)
        {
            var selectQuery = "select name, lname, inc from region where inc in (select region from excurs)";

            //текущий язык
            DataSet set = DatabaseOperationProvider.Query(selectQuery, "regions", new { });

            var regions = new List<KeyValuePair<string, string>>();

            foreach (DataRow row in set.Tables["regions"].Rows)
            {
                var regionslug = StringsHelper.GenerateSlug(row["lname"].ToString());

                if (regionslug == slug)
                    return row.ReadInt("inc");
            }

            return null;
        }

        //список стран, в которых есть экскурсии
        private KeyValuePair<string, string>[] GetCountriesList()
        {
            var selectQuery = "select name, lname, inc from state where inc in (select state from region where inc in (select region from excurs))";

            //текущий язык
            DataSet set = DatabaseOperationProvider.Query(selectQuery, "regions", new { });

            var countries = new List<KeyValuePair<string, string>>();

            foreach (DataRow row in set.Tables["regions"].Rows)
            {
                var slug = StringsHelper.GenerateSlug(row["lname"].ToString());

                if (slug == "") continue;

                if (UrlLanguage.CurrentLanguage == "ru")
                    countries.Add(new KeyValuePair<string, string>(slug, row["name"].ToString()));
                else
                    countries.Add(new KeyValuePair<string, string>(slug, row["lname"].ToString()));
            }

            return countries.ToArray();
        }

        //список регионов, по стране
        private KeyValuePair<string, string>[] GetCountryRegions(string slug)
        {
            int? countryID = GetCountryBySlug(slug);

            if (countryID.HasValue)
            {
                var selectQuery = "select name, lname, inc from region where inc in (select region from excurs) AND state = "  + countryID;
                //текущий язык
                DataSet set = DatabaseOperationProvider.Query(selectQuery, "regions", new { });

                var regions = new List<KeyValuePair<string, string>>();

                foreach (DataRow row in set.Tables["regions"].Rows)
                {
                    var regionslug = StringsHelper.GenerateSlug(row["lname"].ToString());

                    if (UrlLanguage.CurrentLanguage == "ru")
                        regions.Add(new KeyValuePair<string, string>(regionslug, row["name"].ToString()));
                    else
                        regions.Add(new KeyValuePair<string, string>(regionslug, row["lname"].ToString()));
                }

                return regions.ToArray();
            }

            return null;
        }

        //список стран
        [HttpGet, ActionName("index")]
        public ActionResult Index(string country)
        {
            //ищем список стран, в которых есть экскурсии
            if (string.IsNullOrEmpty(country))
            {
                var model = new CountriesCatalog() {
                        Countries = GetCountriesList(),
                        Description = "Каталог стран проекта ExGo",
                        Title = "Каталог стран",
                        Keywords = "Каталог, купить экскурсию",
                        SeoText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum."
                };

                return base.View(model);
            }
            else
            {
                var model = new CountryRegions() {
                    CountryName = country,
                    CountryId = country,
                    Description = "Каталог стран проекта ExGo " + country,
                    Title = "Каталог стран " + country,
                    Keywords = "Каталог, купить экскурсию, " + country,
                    SeoText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Regions = GetCountryRegions(country)
                };

                return base.View("country", model);
            }
        }
    }
}
