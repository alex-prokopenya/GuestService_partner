namespace GuestService.Controllers.Api
{
    using GuestService.Data;
    using GuestService.Models.Guest;
    using Sm.System.Mvc.Language;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    [HttpUrlLanguage, Authorize]
    public class GuestController : ApiController
    {
        [AllowAnonymous, HttpGet, ActionName("departure")]
        public List<DepartureHotel> Departure([FromUri] DepartureParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (!param.FirstDate.HasValue)
            {
                throw new ArgumentNullException("fd");
            }
            if (!param.LastDate.HasValue)
            {
                throw new ArgumentNullException("ld");
            }
            if (!(param.Hotel.HasValue || param.Claim.HasValue))
            {
                throw new ArgumentException("'h' or 'c' should be specified");
            }
            return GuestProvider.GetDepartureInfo(param.Language, param.FirstDate.Value, param.LastDate.Value, param.Hotel, param.Claim);
        }
    }
}

