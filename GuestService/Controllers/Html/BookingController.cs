namespace GuestService.Controllers.Html
{
    using GuestService;
    using GuestService.Code;
    using GuestService.Controllers.Api;
    using GuestService.Data;
    using GuestService.Models.Booking;
    using GuestService.Resources;
    using Newtonsoft.Json;
    using Sm.System.Mvc.Language;
    using Sm.System.Trace;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Web.Mvc;
    using WebMatrix.WebData;

    [UrlLanguage, WebSecurityInitializer, HttpPreferences]
    public class BookingController : BaseController
    {
        [HttpGet, ActionName("agreement")]
        public ActionResult Agreement()
        {
            BookingAgreementContext model = new BookingAgreementContext();
            AgreementParam param = new AgreementParam {
                ln = UrlLanguage.CurrentLanguage
            };
            BookingAgreement agreement = new GuestService.Controllers.Api.BookingController().Agreement(param);
            model.Text = (agreement != null) ? agreement.text : null;
            return this.PartialView("_Agreement", model);
        }

        [ActionName("bookingstatus"), HttpGet]
        public JsonResult BookingStatus(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new System.ArgumentNullException("id");
            }
            int? reservation = null;
            string[] errors = null;
            CompleteOperation operation = CompleteOperation.CreateFromSession(base.Session);
            bool isFinished = operation.IsFinished();
            if (isFinished)
            {
                if (operation.OperationResultType == "bookingresult" && operation.OperationResultData != null)
                {
                    BookingCartResult bookingResult = JsonConvert.DeserializeObject<BookingCartResult>(operation.OperationResultData);
                    if (bookingResult != null && bookingResult.Reservation != null)
                    {
                        reservation = bookingResult.Reservation.claimId;
                        if (reservation.HasValue)
                        {
                            base.TempData[string.Format("order.{0}.name", reservation.Value)] = ((bookingResult.Form != null) ? bookingResult.Form.CustomerName : "");

                            try
                            {
                                //разрешаем пользователю смотреть путевку в экране оплат
                                if (System.Web.HttpContext.Current.Session["allowed_claims"] == null)
                                    System.Web.HttpContext.Current.Session["allowed_claims"] = new List<int>();

                                //  System.Web.HttpContext.Current
                                var list = (System.Web.HttpContext.Current.Session["allowed_claims"] as List<int>);

                                list.Add(bookingResult.Reservation.claimId.Value);

                                System.Web.HttpContext.Current.Session["allowed_claims"] = list;

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            operation.Clear();
                            using (ShoppingCart cart = ShoppingCart.CreateFromSession(base.Session))
                            {
                                cart.Clear();
                            }
                        }
                        else
                        {
                            if (bookingResult.Reservation.errors != null)
                            {
                                errors = (
                                    from m in bookingResult.Reservation.errors
                                    select (!string.IsNullOrEmpty(m.usermessage)) ? m.usermessage : m.message).ToArray<string>();
                            }
                        }
                    }
                }
            }
            return base.Json(new
            {
                isfinished = isFinished,
                reservation = reservation,
                errors = errors
            }, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(NoStore=true, Duration=0, VaryByParam="*"), ActionName("index"), HttpGet]
        public ActionResult Index(BookingCartWebParam param)
        {
            Action<BookingOrder> action = null;
            Action<BookingOrder> action2 = null;
            BookingContext context = new BookingContext();
            CompleteOperation operation = CompleteOperation.CreateFromSession(base.Session);
            if (operation.IsProgress)
            {
                context.BookingOperationId = operation.OperationId;
                return base.View("_BookingProcessing", context);
            }
            if (operation.HasResult)
            {
                BookingCartResult result = JsonConvert.DeserializeObject<BookingCartResult>(operation.OperationResultData);
                if (result != null)
                {
                    context.Prepare(result.Form, result.Reservation);
                }
                operation.Clear();
                return base.View(context);
            }
            using (ShoppingCart cart = ShoppingCart.CreateFromSession(base.Session))
            {
                if (param != null)
                {
                    if (param.PartnerSessionID != null)
                    {
                        cart.PartnerSessionID = param.PartnerSessionID;
                    }
                    if (param.PartnerAlias != null)
                    {
                        cart.PartnerAlias = param.PartnerAlias;
                    }
                }
                context.Form = new BookingModel();
                context.Form.PartnerAlias = cart.PartnerAlias;
                context.Form.PartnerSessionID = cart.PartnerSessionID;
                if (WebSecurity.IsAuthenticated)
                {
                    context.Form.CustomerEmail = WebSecurity.CurrentUserName;
                }
                if (cart.Orders != null)
                {
                    if (action == null)
                    {
                        action = delegate (BookingOrder o) {
                            if (((o != null) && (o.excursion != null)) && (o.excursion.contact != null))
                            {
                                if (!((context.Form.CustomerName != null) || string.IsNullOrEmpty(o.excursion.contact.name)))
                                {
                                    context.Form.CustomerName = o.excursion.contact.name;
                                }
                                if (!((context.Form.CustomerMobile != null) || string.IsNullOrEmpty(o.excursion.contact.mobile)))
                                {
                                    context.Form.CustomerMobile = o.excursion.contact.mobile;
                                }
                            }
                        };
                    }
                    cart.Orders.ForEach(action);
                }
                if (cart.Orders != null)
                {
                    if (action2 == null)
                    {
                        action2 = delegate (BookingOrder m) {
                            BookingOrderModel item = new BookingOrderModel {
                                BookingOrder = m
                            };
                            context.Form.Orders.Add(item);
                        };
                    }
                    cart.Orders.ForEach(action2);
                }
            }
            context.Form.Action = "calculate";
            context.PaymentModes = BookingProvider.GetPaymentModes(UrlLanguage.CurrentLanguage, 2025654180);

            return this.Index(context.Form);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("index")]
        public ActionResult Index([Bind(Prefix="Form")] BookingModel form)
        {
            Predicate<BookingOrderModel> match = null;
            Predicate<BookingOrder> predicate2 = null;
            Action<BookingOrderModel> action2 = null;
            if (form == null)
            {
                throw new ArgumentNullException("form");
            }
            if (form.Action == "remove")
            {
                base.ModelState.Clear();
                if ((form.RemoveOrderId != null) && (form.Orders != null))
                {
                    if (match == null)
                    {
                        match = m => ((m != null) && (m.BookingOrder != null)) && (m.BookingOrder.orderid == form.RemoveOrderId);
                    }
                    form.Orders.RemoveAll(match);
                    using (ShoppingCart cart = ShoppingCart.CreateFromSession(base.Session))
                    {
                        if ((cart != null) && (cart.Orders != null))
                        {
                            if (predicate2 == null)
                            {
                                predicate2 = m => m.orderid == form.RemoveOrderId;
                            }
                            cart.Orders.RemoveAll(predicate2);
                        }
                    }
                }
            }
            BookingContext model = new BookingContext();
            model.PaymentModes = BookingProvider.GetPaymentModes(UrlLanguage.CurrentLanguage, 2025654180);
            BookingClaim bookingClaim = new BookingClaim {
                orders = new List<BookingOrder>()
            };
            if (form.Orders != null)
            {
                if (action2 == null)
                {
                    action2 = delegate (BookingOrderModel m) {
                        if ((m != null) && (m.BookingOrder != null))
                        {
                            bookingClaim.orders.Add(m.BookingOrder);
                        }
                    };
                }
                form.Orders.ForEach(action2);
            }
            BookingCartParam bookingCartParam = new BookingCartParam {
                ln = UrlLanguage.CurrentLanguage,
                pa = form.PartnerAlias,
                psid = form.PartnerSessionID
            };
            GuestService.Controllers.Api.BookingController controller = new GuestService.Controllers.Api.BookingController();
            bookingClaim.note = form.BookingNote;
            Customer customer = new Customer {
                name = form.CustomerName,
                mobile = form.CustomerMobile,
                email = form.CustomerEmail,
                address = form.CustomerAddress
            };
            bookingClaim.customer = customer;
            if (form.PromoCodes != null)
            {
                bookingClaim.PromoCodes = new List<string>(form.PromoCodes);
            }
            if (form.Action == null)
            {
                if (!form.RulesAccepted)
                {
                    base.ModelState.AddModelError("Form.RulesAccepted", BookingStrings.RulesAccepted);
                }
                if (base.ModelState.IsValid)
                {
                    CompleteOperation operation = CompleteOperation.CreateFromSession(base.Session);
                    operation.Start();
                    model.BookingOperationId = operation.OperationId;
                    int? userId = WebSecurity.IsAuthenticated ? new int?(WebSecurity.CurrentUserId) : null;
                    ThreadPool.QueueUserWorkItem(delegate (object o) {
                        try
                        {
                            BookingCartResult result = new BookingCartResult {
                                Form = form,
                                Reservation = controller.Book(bookingCartParam, bookingClaim)
                            };
                            if (((result.Reservation != null) && result.Reservation.claimId.HasValue) && userId.HasValue)
                            {
                                GuestProvider.LinkGuestClaim(userId.Value, result.Reservation.claimId.Value);
                            }
                            string data = JsonConvert.SerializeObject(result);
                            CompleteOperationProvider.SetResult(operation.OperationId, "bookingresult", data);
                        }
                        catch (Exception exception)
                        {
                            Tracing.WebTrace.TraceEvent(TraceEventType.Error, 2, exception.ToString());
                            CompleteOperationProvider.SetResult(operation.OperationId, null, null);
                        }
                    }, null);
                   
                    return base.View("_BookingProcessing", model);
                }
            }
            else if (form.Action == "promo")
            {
                base.ModelState.Clear();
                List<string> list = (form.PromoCodes == null) ? new List<string>() : new List<string>(form.PromoCodes);
                if (!string.IsNullOrWhiteSpace(form.PromoCode))
                {
                    Action<BookingOrderModel> action = null;
                    BookingClaim checkPromoClaim = new BookingClaim {
                        orders = new List<BookingOrder>()
                    };
                    if (form.Orders != null)
                    {
                        if (action == null)
                        {
                            action = delegate (BookingOrderModel m) {
                                if ((m != null) && (m.BookingOrder != null))
                                {
                                    checkPromoClaim.orders.Add(m.BookingOrder);
                                }
                            };
                        }
                        form.Orders.ForEach(action);
                    }
                    checkPromoClaim.PromoCodes = (form.PromoCodes != null) ? new List<string>(form.PromoCodes) : new List<string>();
                    CheckPromoCodeResult result = controller.CheckPromoCode(bookingCartParam, bookingClaim, form.PromoCode);
                    if ((result != null) && (result.errorcode == 0))
                    {
                        list.Add(form.PromoCode);
                        bookingClaim.PromoCodes = list;
                        form.PromoCodes = list.ToArray();
                    }
                    else
                    {
                        base.ModelState.AddModelError("PromoCodeError", (result != null) ? result.errormessage : "невозможно применить промо код");
                    }
                }
            }
            ReservationState reservation = controller.Calculate(bookingCartParam, bookingClaim);
            model.Prepare(form, reservation);
            return base.View(model);
        }
    }
}

