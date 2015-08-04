using GuestService;
using GuestService.Data;
using GuestService.Models.Booking;
using Sm.System.Exceptions;
using Sm.System.Mvc.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebMatrix.WebData;

namespace GuestService.Controllers.Api
{


    [HttpUrlLanguage, Authorize]
    public class BookingController : ApiController
    {
        [HttpGet, AllowAnonymous, ActionName("agreement")]
        public BookingAgreement Agreement([FromUri] AgreementParam param)
        {
            BookingAgreement agreement = new BookingAgreement();
            string str = HttpContext.Current.Server.MapPath(CustomizationPath.AgreementsFolder);
            string path = Path.Combine(str, string.Format("Booking{0}{1}.txt", ".", UrlLanguage.CurrentLanguage));
            if (File.Exists(path))
            {
                agreement.text = File.ReadAllText(path);
                return agreement;
            }
            path = Path.Combine(str, string.Format("Booking{0}{1}.txt", "", ""));
            if (File.Exists(path))
            {
                agreement.text = File.ReadAllText(path);
            }
            return agreement;
        }

        [AllowAnonymous, HttpPost, ActionName("book")]
        public ReservationState Book([FromUri] BookingCartParam param, [FromBody] BookingClaim claim)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            if (claim.customer != null)
            {
                foreach (BookingOrder order in claim.orders)
                {
                    if (order.excursion != null)
                    {
                        if (order.excursion.contact == null)
                        {
                            order.excursion.contact = new ExcursionContact();
                        }
                        if (order.excursion.contact.name == null)
                        {
                            order.excursion.contact.name = claim.customer.name;
                        }
                        if (order.excursion.contact.mobile == null)
                        {
                            order.excursion.contact.mobile = claim.customer.mobile;
                        }
                    }
                }
            }
            return BookingProvider.DoBooking(param.Language, partner.id, partner.passId, claim);
        }

        [HttpPost, ActionName("calculate"), AllowAnonymous]
        public ReservationState Calculate([FromUri] BookingCartParam param, [FromBody] BookingClaim claim)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            return BookingProvider.DoCalculation(param.Language, partner.id, claim);
        }

        [ActionName("checkpromocode"), HttpPost, AllowAnonymous]
        public CheckPromoCodeResult CheckPromoCode([FromUri] BookingCartParam param, [FromBody] BookingClaim claim, [FromUri] string promocode)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            if (string.IsNullOrEmpty(promocode))
            {
                throw new ArgumentNullException("promocode");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            return BookingProvider.CheckExcursionPromoCode(param.Language, partner.id, claim, promocode);
        }

        [ActionName("state"), AllowAnonymous, HttpGet]
        public ReservationState State(int? id, [FromUri] StatusParams param)
        {
            Func<GuestClaim, bool> func = null;
            if (!id.HasValue)
            {
                throw new ArgumentNullExceptionWithCode(110, "id");
            }
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            WebPartner partner = UserToolsProvider.GetPartner(param);
            ReservationState result = BookingProvider.GetReservationState(param.Language, id.Value);
            ReservationState result2;
            if (result != null && result.claimId.HasValue)
            {
                if (param.PartnerSessionID != null && result.partner != null && result.partner.id == partner.id)
                {
                    result2 = result;
                    return result2;
                }
                if (WebSecurity.CurrentUserId > 0)
                {
                    System.Collections.Generic.List<GuestClaim> claims = GuestProvider.GetLinkedClaims(param.Language, WebSecurity.CurrentUserId);
                    bool arg_10E_0;
                    if (claims != null)
                    {
                        arg_10E_0 = (claims.FirstOrDefault((GuestClaim m) => m.claim == result.claimId.Value) == null);
                    }
                    else
                    {
                        arg_10E_0 = true;
                    }
                    if (!arg_10E_0)
                    {
                        result2 = result;
                        return result2;
                    }
                }
            }
            return null;
        }
    }
}

