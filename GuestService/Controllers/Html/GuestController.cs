namespace GuestService.Controllers.Html
{
    using GuestService;
    using GuestService.Code;
    using GuestService.Controllers.Api;
    using GuestService.Data;
    using GuestService.Models.Guest;
    using GuestService.Models.Guide;
    using Sm.Report;
    using Sm.System.Mvc;
    using Sm.System.Mvc.Language;
    using Sm.System.Trace;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using WebMatrix.WebData;

    [UrlLanguage, Authorize, HttpPreferences, WebSecurityInitializer]
    public class GuestController : BaseController
    {
        [AllowAnonymous, ActionName("brief"), HttpGet]
        public ActionResult Brief(GuestWebParams param)
        {
            BriefContext model = new BriefContext();
            return base.View(model);
        }

        private ActionResult BuildVoucher(int claimId)
        {
            ActionResult result3;
            try
            {
                List<ReportParam> list = new List<ReportParam>();
                ReportParam item = new ReportParam {
                    Name = "vClaimList",
                    Value = claimId.ToString()
                };
                list.Add(item);
                string str = ConfigurationManager.AppSettings["report_PrintVoucher"];
                if (string.IsNullOrEmpty(str))
                {
                    throw new Exception("report_PrintVoucher is empty");
                }
                ReportResult result = ReportServer.BuildReport(str, ReportFormat.pdf, list.ToArray());
                if (result == null)
                {
                    throw new Exception("report data is empty");
                }
                MemoryStream fileStream = new MemoryStream(result.Content);
                FileStreamResult result2 = new FileStreamResult(fileStream, "application/pdf") {
                    FileDownloadName = string.Format("voucher_{0}.pdf", claimId)
                };
                result3 = result2;
            }
            catch (Exception exception)
            {
                Tracing.ServiceTrace.TraceEvent(TraceEventType.Error, 0, exception.ToString());
                throw;
            }
            return result3;
        }

        private void ClearErrorState(ModelState modelState)
        {
            if (modelState != null)
            {
                modelState.Errors.Clear();
            }
        }

        [ActionName("departure"), AllowAnonymous, HttpGet]
        public ActionResult Departure(GuestWebParams param)
        {
            DepartureContext context;
            DateTime date = ((param != null) && param.TestDate.HasValue) ? param.TestDate.Value.Date : DateTime.Now.Date;
            if (WebSecurity.IsAuthenticated)
            {
                context = new DepartureContext();
                List<GuestClaim> list = GuestProvider.GetActiveClaims(UrlLanguage.CurrentLanguage, WebSecurity.CurrentUserId, date);
                if ((list != null) && (list.Count > 0))
                {
                    context.Hotels = new List<DepartureHotel>();
                    foreach (GuestClaim claim in list)
                    {
                        int? hotel = null;
                        context.Hotels.AddRange(GuestProvider.GetDepartureInfo(UrlLanguage.CurrentLanguage, date, date.AddDays(1.0), hotel, new int?(claim.claim)));
                    }
                }
                return base.View(context);
            }
            if (HttpPreferences.Current.LocationHotel != null)
            {
                context = new DepartureContext {
                    Hotel = CatalogProvider.GetHotelDescription(UrlLanguage.CurrentLanguage, HttpPreferences.Current.LocationHotel)
                };
                if (context.Hotel != null)
                {
                    context.Hotels = GuestProvider.GetDepartureInfo(UrlLanguage.CurrentLanguage, date, date.AddDays(1.0), new int?(context.Hotel.id), null);
                }
                return base.View(context);
            }
            string str = base.Url.RouteUrl(base.Request.QueryStringAsRouteValues());
            return base.RedirectToAction("login", "account", new { returnUrl = str });
        }

        [ActionName("findorder"), HttpGet]
        public ActionResult FindOrder()
        {
            FindOrderContext model = new FindOrderContext();
            return base.View(model);
        }

        [ValidateAntiForgeryToken, HttpPost, ActionName("findorder")]
        public ActionResult FindOrderPost(string id, [Bind(Prefix = "Form")] FindOrderModel form)
        {
            FindOrderContext context = new FindOrderContext();
            context.Form = form;
            if (form.RequestType != "claim")
            {
                this.ClearErrorState(base.ModelState["Form.Claim"]);
                this.ClearErrorState(base.ModelState["Form.ClaimName"]);
            }
            if (form.RequestType != "passport")
            {
                this.ClearErrorState(base.ModelState["Form.Passport"]);
                this.ClearErrorState(base.ModelState["Form.PassportName"]);
            }
            if (base.ModelState.IsValid)
            {
                int guestId = WebSecurity.CurrentUserId;
                if (form.RequestType == "claim")
                {
                    context.Claims = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, guestId, form.ClaimName, new int?(System.Convert.ToInt32(form.Claim)), null);
                }
                else
                {
                    if (!(form.RequestType == "passport"))
                    {
                        throw new System.Exception("invalid RequestType");
                    }
                    context.Claims = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, guestId, form.PassportName, null, form.Passport);
                }
                context.NotFound = (context.Claims.Count == 0);
            }
            return base.View(context);
        }

        [AllowAnonymous, ActionName("index"), HttpGet]
        public ActionResult Index(GuestWebParams param)
        {
            GuestContext model = new GuestContext();
            if (WebSecurity.IsAuthenticated)
            {
                DateTime firstDate = ((param != null) && param.TestDate.HasValue) ? param.TestDate.Value.Date : DateTime.Now.Date;
                model.GuideDurties = new List<HotelGuideResult>();
                List<GuestOrder> list = GuestProvider.GetActiveHotelOrders(UrlLanguage.CurrentLanguage, WebSecurity.CurrentUserId, firstDate, firstDate.AddDays(1.0));
                if ((list != null) && (list.Count > 0))
                {
                    GuideController controller = new GuideController();
                    foreach (GuestOrder order in list)
                    {
                        HotelGuideParam param2 = new HotelGuideParam {
                            h = order.hotelid,
                            ln = UrlLanguage.CurrentLanguage,
                            pb = new DateTime?(order.period.begin.Value),
                            pe = new DateTime?(order.period.end.Value)
                        };
                        HotelGuideResult item = controller.HotelGuide(param2);
                        model.GuideDurties.Add(item);
                    }
                }
            }
            else
            {
                model.ShowAuthenticationMessage = true;
            }
            return base.View(model);
        }

        [HttpPost, ActionName("linkorder"), ValidateAntiForgeryToken]
        public ActionResult LinkOrder([Bind(Prefix="Link")] LinkOrderModel model)
        {
            if ((model != null) && model.Claim.HasValue)
            {
                GuestProvider.LinkGuestClaim(WebSecurity.CurrentUserId, model.Name, model.Claim.Value);
                return base.RedirectToAction("order", new { id = model.Claim });
            }
            return base.RedirectToAction("order");
        }

        [HttpPost, ActionName("order")]
        public ActionResult Order([Bind(Prefix="OrderFindForm")] OrderModel model)
        {
            OrderContext context = new OrderContext {
                ShowOrderFindForm = true
            };
            if (base.ModelState.IsValid)
            {
                List<GuestClaim> list = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, 0, model.ClaimName, new int?(Convert.ToInt32(model.Claim)), null);
                if ((list != null) && (list.Count > 0))
                {
                    ReservationState reservationState = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, list[0].claim);
                    if ((reservationState != null) && reservationState.claimId.HasValue)
                    {
                        context.Claim = reservationState;
                        context.ExcursionTransfers = GuestProvider.GetExcursionTransferByClaim(UrlLanguage.CurrentLanguage, reservationState.claimId.Value);
                        context.ShowOrderFindForm = false;
                    }
                }
            }
            context.OrderFindNotFound = context.Claim == null;
            return base.View(context);
        }

        [HttpGet, ActionName("order")]
        public ActionResult Order(int? id)
        {



            OrderContext model = new OrderContext();
            int currentUserId = WebSecurity.CurrentUserId;
            List<GuestClaim> linkedClaims = GuestProvider.GetLinkedClaims(UrlLanguage.CurrentLanguage, currentUserId);
            int? detailedId = null;
            if (id.HasValue)
            {
                if (linkedClaims.FirstOrDefault((GuestClaim m) => m.claim == id.Value) != null)
                {
                    detailedId = new int?(id.Value);
                }
            }

            if (!(detailedId.HasValue || (linkedClaims.Count <= 0)))
            {
                detailedId = new int?(linkedClaims[0].claim);
            }
            if (detailedId.HasValue)
            {
                ReservationState reservationState = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, detailedId.Value);
                if ((reservationState != null) && reservationState.claimId.HasValue)
                {
                    model.Claim = reservationState;
                    model.ExcursionTransfers = GuestProvider.GetExcursionTransferByClaim(UrlLanguage.CurrentLanguage, reservationState.claimId.Value);
                }
            }
            model.ClaimsNotFound = linkedClaims.Count == 0;
            model.ShowOtherClaims = true;

            model.OtherClaims = (
                    from m in linkedClaims

          //удалить детальн
          //  where m.claim != detailedId
                  select m).ToList<GuestClaim>();
            //     model.OtherClaims = linkedClaims.Where<GuestClaim>(new Func<GuestClaim, bool>(class2, (IntPtr) this.<Order>b__2)).ToList<GuestClaim>();


            return base.View(model);
        }

        [AllowAnonymous, ActionName("printorder")]
        public ActionResult PrintOrder(int? id)
        {
            PrintOrderContext context = new PrintOrderContext();
            context.Form = new PrintOrderModel();
            context.Form.Claim = (id.HasValue ? id.ToString() : "");
            ActionResult result;
            if (WebSecurity.IsAuthenticated)
            {
                int guestId = WebSecurity.CurrentUserId;
                System.Collections.Generic.List<GuestClaim> claims = GuestProvider.GetLinkedClaims(UrlLanguage.CurrentLanguage, guestId);
                int? detailedId = null;
                if (id.HasValue)
                {
                    if (claims.FirstOrDefault((GuestClaim m) => m.claim == id.Value) != null)
                    {
                        detailedId = new int?(id.Value);
                    }
                }
                if (detailedId.HasValue)
                {
                    result = this.BuildVoucher(detailedId.Value);
                    return result;
                }
                context.NotFound = true;
            }
            result = base.View(context);
            return result;
        }

        [HttpPost, AllowAnonymous, ActionName("printorder")]
        public ActionResult PrintOrderPost([Bind(Prefix="Form")] PrintOrderModel model)
        {
            PrintOrderContext context = new PrintOrderContext {
                Form = model
            };
            int result = 0;
            if (base.ModelState.IsValid && int.TryParse(model.Claim, out result))
            {
                List<GuestClaim> list = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, 0, model.Name, new int?(result), null);
                if ((list != null) && (list.Count > 0))
                {
                    return this.BuildVoucher(result);
                }
                context.NotFound = true;
            }
            return base.View(context);
        }

        [ActionName("summary"), AllowAnonymous, HttpGet]
        public ActionResult Summary(GuestWebParams param)
        {
            SummaryContext model = new SummaryContext {
                ShowOrderFindForm = true,
                OrderFindForm = new OrderModel()
            };
            model.OrderFindForm.Claim = "";
            model.OrderFindForm.ClaimName = "";
            model.OrderFindForm.CurrentDate = new DateTime?(((param != null) && param.TestDate.HasValue) ? param.TestDate.Value.Date : DateTime.Now.Date);
            return base.View(model);
        }

        [ActionName("summary"), AllowAnonymous, HttpPost]
        public ActionResult Summary([Bind(Prefix="OrderFindForm")] OrderModel model)
        {
            SummaryContext context = new SummaryContext {
                ShowOrderFindForm = true
            };
            if (base.ModelState.IsValid)
            {
                DateTime? currentDate = model.CurrentDate;
                DateTime dateFrom = currentDate.HasValue ? currentDate.GetValueOrDefault() : DateTime.Now.Date;
                List<GuestClaim> claims = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, 0, model.ClaimName, new int?(Convert.ToInt32(model.Claim)), null);
                if ((claims != null) && (claims.Count > 0))
                {
                    int? nullable2;
                    ReservationState reservationState = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, claims[0].claim);
                    if ((reservationState != null) && (nullable2 = reservationState.claimId).HasValue)
                    {
                        context.Claim = reservationState;
                        context.ShowOrderFindForm = false;
                        context.Hotels = new List<DepartureHotel>();
                        foreach (GuestClaim claim in claims)
                        {
                            nullable2 = null;
                            context.Hotels.AddRange(GuestProvider.GetDepartureInfo(UrlLanguage.CurrentLanguage, dateFrom, dateFrom.AddDays(1.0), nullable2, new int?(claim.claim)));
                        }
                        context.GuideDurties = new List<HotelGuideResult>();
                        List<GuestOrder> list2 = GuestProvider.GetActiveHotelOrders(claims, dateFrom, dateFrom.AddDays(1.0));
                        if ((list2 != null) && (list2.Count > 0))
                        {
                            GuideController controller = new GuideController();
                            foreach (GuestOrder order in list2)
                            {
                                HotelGuideParam param = new HotelGuideParam {
                                    h = order.hotelid,
                                    ln = UrlLanguage.CurrentLanguage,
                                    pb = new DateTime?(order.period.begin.Value),
                                    pe = new DateTime?(order.period.end.Value)
                                };
                                HotelGuideResult item = controller.HotelGuide(param);
                                context.GuideDurties.Add(item);
                            }
                        }
                    }
                }
            }
            context.OrderFindNotFound = context.Claim == null;
            return base.View(context);
        }

        [ActionName("unlinkorder"), HttpPost, ValidateAntiForgeryToken]
        public ActionResult UnlinkOrder([Bind(Prefix="Unlink")] UnlinkOrderModel model)
        {
            if ((model != null) && model.Claim.HasValue)
            {
                GuestProvider.UnlinkGuestClaim(WebSecurity.CurrentUserId, model.Claim.Value);
            }
            return base.RedirectToAction("order");
        }
    }
}

