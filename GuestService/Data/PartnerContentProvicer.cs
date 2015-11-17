using Sm.System.Database;
using Sm.System.Profiling;
using Sm.System.Mvc.Language;
using Sm.System.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Web;
using Sm.System.Mvc.Language;
using GuestService.Notifications;


namespace GuestService.Data
{
    public class PartnerContentProvider
    {
        private PartnerContent PartnerContent(DataRow row)
        {
            return new PartnerContent()
            {
                Description = row.ReadNullableTrimmedString("description"),
                Title = row.ReadNullableTrimmedString("title"),
                Keywords = row.ReadNullableTrimmedString("keywords"),
                Footer = row.ReadNullableTrimmedString("footer"),
                Header = row.ReadNullableTrimmedString("header"),
                ImageName = row.ReadNullableTrimmedString("background_image_name"),
                PartnerId = row.ReadInt("partner_id"),
                UserId = row.ReadInt("partner_user_id")
            };
        }

        private static void AddPartnerContent(ref PartnerContent defaultContent, DataRow row)
        {
            if(!row.IsNull("description"))
                defaultContent.Description = row.ReadNullableTrimmedString("description");

            if (!row.IsNull("title"))
                defaultContent.Title = row.ReadNullableTrimmedString("title");

            if (!row.IsNull("keywords"))
                defaultContent.Keywords = row.ReadNullableTrimmedString("keywords");

            if (!row.IsNull("footer"))
                defaultContent.Footer = row.ReadNullableTrimmedString("footer");

            if (!row.IsNull("header"))
                defaultContent.Header = row.ReadNullableTrimmedString("header");

            if (!row.IsNull("background_image_name"))
                defaultContent.ImageName = row.ReadNullableTrimmedString("background_image_name");

            if (!row.IsNull("partner_id"))
                defaultContent.PartnerId = row.ReadInt("partner_id");

            if (!row.IsNull("partner_user_id"))
                defaultContent.UserId = row.ReadInt("partner_user_id");
        }

        public static PartnerContent GetPartnerContent(string lang) {
            //current domain

            try
            {
                var domain = "excursions.ru-kipr.ru";

                //domain = HttpContext.Current.Request.Url.Host;
                //current page
                //current language

                var query = "   SELECT  partner_id, partner_user_id, header, footer, " +
                            "           background_image_name, title, keywords, description " +

                            "   FROM guestservice_partner_data, guestservice_partner_links " +

                            "   WHERE partner_domain = @domain and user_id = partner_user_id and lang_code = @lang and special_for_page = @page";

                var partnerContent = new PartnerContent();

                var res = DatabaseOperationProvider.Query(query, "content", new { lang = lang, domain = domain, page = "" });

                if (res.Tables["content"].Rows.Count > 0)
                    AddPartnerContent(ref partnerContent, res.Tables["content"].Rows[0]);

                res = DatabaseOperationProvider.Query(query, "content", new { lang = lang, domain = domain, page = HttpContext.Current.Request.Url.AbsolutePath });

                if (res.Tables["content"].Rows.Count > 0)
                    AddPartnerContent(ref partnerContent, res.Tables["content"].Rows[0]);

                return partnerContent;
            }
            catch (Exception ex)
            {
                return new Data.PartnerContent();
            }
        }
    }
}
