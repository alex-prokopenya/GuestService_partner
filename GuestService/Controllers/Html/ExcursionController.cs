namespace GuestService.Controllers.Html
{
    using GuestService;
    using GuestService.Code;
    using GuestService.Data;
    using GuestService.Data.Survey;
    using GuestService.Models.Booking;
    using GuestService.Models.Excursion;
    using GuestService.Resources;
    using Sm.System.Mvc.Language;
    using Sm.System.Mvc.Theme;
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    [UrlLanguage, HttpPreferences, WebSecurityInitializer]
    public class ExcursionController : BaseController
    {
        [AllowAnonymous, HttpGet, ActionName("logout")]
        public ActionResult Logout(ExcursionIndexWebParam param)
        {
            base.Session.Abandon();

            ExcursionIndexContext context = new ExcursionIndexContext();

            return base.View(context);
        }

        [AllowAnonymous, HttpGet, ActionName("howtobook")]
        public ActionResult HowToBook(ExcursionIndexWebParam param)
        {
           
            ExcursionIndexContext context = new ExcursionIndexContext();

            return base.View(context);
        }

        [AllowAnonymous, HttpGet, ActionName("howtopay")]
        public ActionResult HowToPay(ExcursionIndexWebParam param)
        {
            ExcursionIndexContext context = new ExcursionIndexContext();
            
            return base.View(context);
        }


        [HttpPost, ActionName("addcart")]
        public JsonResult AddCart(ExcursionAddWebParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            List<string> list = new List<string>();
            if (param.excursion != null)
            {
                if ((param.excursion.pax != null) && ((param.excursion.pax.adult + param.excursion.pax.child) > 0))
                {
                    if (param.cid == null)
                    {
                        int? nullable = null;
                        using (ShoppingCart cart = ShoppingCart.CreateFromSession(base.Session))
                        {
                            cart.PartnerAlias = param.PartnerAlias;
                            cart.PartnerSessionID = param.psid;
                            BookingOrder item = new BookingOrder {
                                orderid = Guid.NewGuid().ToString(),
                                excursion = param.excursion
                            };
                            cart.Orders.Add(item);
                            nullable = new int?(cart.Orders.Count);
                        }
                        return base.Json(new { ok = true, ordercount = nullable });
                    }
                    WebPartner partner = UserToolsProvider.GetPartner(param);
                    BookingOrder order2 = new BookingOrder {
                        orderid = Guid.NewGuid().ToString(),
                        excursion = param.excursion
                    };
                    ExternalCartAddOrderResult result = BookingProvider.ExternalCart_AddOrder(UrlLanguage.CurrentLanguage, partner, param.cid, order2);
                    if (result.errorcode == 0)
                    {
                        return base.Json(new { ok = true, ordercount = result.ordercount });
                    }
                    list.Add(result.errormessage);
                }
                else
                {
                    list.Add(ExcursionStrings.ResourceManager.Get("ErrorGuestCount"));
                }
            }
            else
            {
                list.Add(ExcursionStrings.ResourceManager.Get("ErrorInvalidParams"));
            }
            return base.Json(new { errormessages = list.ToArray() });
        }

        [ActionName("addrating"), HttpPost]
        public ActionResult AddRating(int? id)
        {
            if (Settings.IsAddRankingEnabled && id.HasValue)
            {
                List<ExcursionDescription> description = ExcursionProvider.GetDescription(UrlLanguage.CurrentLanguage, new int[] { id.Value });
                if ((description != null) && (description.Count == 1))
                {
                    ExcursionInvitation invitation = SurveyProvider.CreateInvitation(id.Value, description[0].excursion.name, UrlLanguage.CurrentLanguage);
                    if (invitation != null)
                    {
                        return base.RedirectToAction("index", "survey", new { id = invitation.AccessCode });
                    }
                }
            }
            return base.RedirectToAction("index");
        }

        [AllowAnonymous, HttpGet, ActionName("details")]
        public ActionResult Details(ExcursionIndexWebParam param, int? id)
        {
            if ((param != null) && (param.visualtheme != null))
            {
                new VisualThemeManager(this).SafeSetThemeName(param.visualtheme);
            }
            ExcursionIndexContext model = new ExcursionIndexContext();
            PartnerSessionManager.CheckPartnerSession(this, param);
            model.PartnerSessionId = param.PartnerSessionID;
            if (model.PartnerSessionId == null)
            {
                model.PartnerAlias = (param.PartnerAlias != null) ? param.PartnerAlias : Settings.ExcursionDefaultPartnerAlias;
                if (string.IsNullOrEmpty(model.PartnerAlias))
                {
                    throw new ArgumentException("partner alias is not specified");
                }
            }
            model.StartPointAlias = param.StartPointAlias;
            model.ExcursionDate = DateTime.Today.Date.AddDays((double)Settings.ExcursionDefaultDate);
            model.ExternalCartId = param.ExternalCartId;

            model.NavigateState = new ExcursionIndexNavigateCommand();
            param.sc = "description";
            param.ex = id;
            param.dt = DateTime.Today.AddDays(2);

            // if ((param.ShowCommand.ToLower() == "description") && param.Excursion.HasValue)
            {
                    model.NavigateState.Cmd = "description";
                    ExcursionIndexNavigateOptions options2 = new ExcursionIndexNavigateOptions
                    {
                        excursion = param.Excursion,
                        date = param.Date
                    };
                    model.NavigateState.Options = options2;
            }

            return base.View(model);
        }

        private int GetDepartByName(string slug)
        {
            var regionId = CountriesController.GetRegionBySlug(slug);

            return  regionId.HasValue ? regionId.Value: 0;
        }
             

        public ActionResult Index(ExcursionIndexWebParam param)
        {
            if ((param != null) && (param.visualtheme != null))
                new VisualThemeManager(this).SafeSetThemeName(param.visualtheme);

            ExcursionIndexContext model = new ExcursionIndexContext();
            PartnerSessionManager.CheckPartnerSession(this, param);
            model.PartnerSessionId = param.PartnerSessionID;
            if (model.PartnerSessionId == null)
            {
                model.PartnerAlias = (param.PartnerAlias != null) ? param.PartnerAlias : Settings.ExcursionDefaultPartnerAlias;
                if (string.IsNullOrEmpty(model.PartnerAlias))
                    throw new ArgumentException("partner alias is not specified");
            }
            model.StartPointAlias = param.StartPointAlias;
            model.ExcursionDate = DateTime.Today.Date.AddDays((double) Settings.ExcursionDefaultDate);
            model.ExternalCartId = param.ExternalCartId;

            if (!string.IsNullOrEmpty(param.region))
            {
                var regionId = GetDepartByName(param.region);

                if (regionId == 0) return RedirectPermanent("/error/404");

                param.sc = "search";
                param.d = new int[] { GetDepartByName(param.region) };
                param.s = "";

                model.Title = "Excursions in " + param.region;
            }

            if (param.ShowCommand != null)
            {
                model.NavigateState = new ExcursionIndexNavigateCommand();
                if (param.ShowCommand.ToLower() == "search")
                {
                    if ((((param.SearchText != null) || (param.Categories != null)) || (param.Destinations != null)) || (param.ExcursionLanguages != null))
                    {
                        model.NavigateState.Cmd = "search";
                        ExcursionIndexNavigateOptions options = new ExcursionIndexNavigateOptions {
                            text = param.SearchText,
                            categories = param.Categories,
                            destinations = param.Destinations,
                            departures = param.Destinations,
                            languages = param.ExcursionLanguages
                        };
                        model.NavigateState.Options = options;
                    }
                }
                else if ((param.ShowCommand.ToLower() == "description") && param.Excursion.HasValue)
                {
                    model.NavigateState.Cmd = "description";
                    ExcursionIndexNavigateOptions options2 = new ExcursionIndexNavigateOptions {
                        excursion = param.Excursion,
                        date = param.Date
                    };
                    model.NavigateState.Options = options2;
                }
            }
            if (param.Excursion.HasValue)
            {
                model.NavigateState.Cmd = "description";
                ExcursionIndexNavigateOptions options2 = new ExcursionIndexNavigateOptions
                {
                    excursion = param.Excursion,
                    date = param.Date
                };
                model.NavigateState.Options = options2;
            }

            return base.View(model);
        }
    }
}

