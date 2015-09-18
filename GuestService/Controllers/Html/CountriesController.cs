namespace GuestService.Controllers.Html
{
    using GuestService;
    using GuestService.Code;
    using GuestService.Models.Countries;
    using Sm.System.Mvc;
    using Sm.System.Mvc.Language;
    using System;
    using System.Web.Mvc;

    [HttpPreferences, WebSecurityInitializer, UrlLanguage]
    public class CountriesController : BaseController
    {
        //список стран
        [HttpGet, ActionName("index")]
        public ActionResult Index(string country)
        {
            //ищем список стран, в которых есть экскурсии
            if (string.IsNullOrEmpty(country))
            {
                var model = new CountriesCatalog() {
                        Countries = new System.Collections.Generic.KeyValuePair<string, string>[] {
                            new System.Collections.Generic.KeyValuePair<string, string>("Беларусь","belarus"),
                            new System.Collections.Generic.KeyValuePair<string, string>("Россия","russia"),
                            new System.Collections.Generic.KeyValuePair<string, string>("Казахстан","kz")
                        },
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
                    Regions = new System.Collections.Generic.KeyValuePair<string, string>[] {
                            new System.Collections.Generic.KeyValuePair<string, string>("Бобруйск","bobruisk"),
                            new System.Collections.Generic.KeyValuePair<string, string>("Барановичи","baranovichi"),
                            new System.Collections.Generic.KeyValuePair<string, string>("Смолевичи","smolevichi")
                   }
                };

                return base.View("country", model);
            }
        }

    }
}
