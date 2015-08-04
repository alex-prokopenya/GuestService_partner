namespace GuestService.Controllers.Api
{
    using GuestService;
    using GuestService.Data;
    using GuestService.Models;
    using GuestService.Models.Guide;
    using GuestService.Resources;
    using Sm.System.Exceptions;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web.Http;

    public class GuideController : ApiController
    {
        [HttpGet, ActionName("hotelguide")]
        public HotelGuideResult HotelGuide([FromUri] HotelGuideParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (!param.Hotel.HasValue)
            {
                throw new ArgumentNullExceptionWithCode(0x6b, "h");
            }
            if (!param.PeriodBegin.HasValue)
            {
                throw new ArgumentNullExceptionWithCode(0x6c, "pb");
            }
            if (!param.PeriodEnd.HasValue)
            {
                throw new ArgumentNullExceptionWithCode(0x6d, "pe");
            }
            return new HotelGuideResult { hotel = CatalogProvider.GetHotelDescription(param.Language, param.Hotel.Value), guides = GuideProvider.GetHotelGuides(param.Language, param.Hotel.Value, param.PeriodBegin.Value, param.PeriodEnd.Value) };
        }

        [HttpGet, ActionName("photo")]
        public HttpResponseMessage Photo(int id, [FromUri] ImageParam param)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            ImageFormatter formatter = new ImageFormatter(GuideProvider.GetGuideImage(id), Pictures.GuideNoPhoto);
            param.ApplyFormat(formatter);
            Stream content = formatter.CreateStream();
            if (content != null)
            {
                message.Content = new StreamContent(content);
                message.Content.Headers.ContentType = new MediaTypeHeaderValue(formatter.MediaType);
                return message;
            }
            message.StatusCode = HttpStatusCode.NotFound;
            return message;
        }
    }
}

