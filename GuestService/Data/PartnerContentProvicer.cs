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
    class PartnerContentProvider
    {
        private PartnerContent PartnerContent(DataRow row)
        {
            return new PartnerContent()
            {
                Description = row.ReadNullableTrimmedString("address"),
                Title = row.ReadNullableTrimmedString("address"),
                Keywords = row.ReadNullableTrimmedString("name"),
                Footer = row.ReadNullableTrimmedString("phones"),
                Header = row.ReadNullableTrimmedString("email"),
                ImageName = row.ReadNullableTrimmedString("email"),
                PartnerId = row.ReadInt("email"),
                UserId = row.ReadInt("email")
            };
        }

        private static void AddPartnerContent(ref PartnerContent defaultContent, DataRow row)
        {
            if(row.IsNull(""))
                defaultContent.Description = row.ReadNullableTrimmedString("description");

            if (row.IsNull(""))
                defaultContent.Title = row.ReadNullableTrimmedString("title");

            if (row.IsNull(""))
                defaultContent.Keywords = row.ReadNullableTrimmedString("keywords");

            if (row.IsNull(""))
                defaultContent.Footer = row.ReadNullableTrimmedString("footer");

            if (row.IsNull(""))
                defaultContent.Header = row.ReadNullableTrimmedString("header");

            if (row.IsNull(""))
                defaultContent.ImageName = row.ReadNullableTrimmedString("background_image_name");

            if (row.IsNull(""))
                defaultContent.PartnerId = row.ReadInt("partner_id");

            if (row.IsNull(""))
                defaultContent.UserId = row.ReadInt("partner_user_id");
        }

        public static PartnerContent GetPartnerContent() {
            //current domain
            //current page
            //current language

            var query = "   SELECT  partner_id, partner_user_id, header, footer, " +
                        "           background_image_name, title, keywords, description " +

                        "   FROM guestservice_partner_data, guestservice_partner_links " +

                        "   WHERE partner_domain = @domain and user_id = partner_user_id and lang_code = @lang and special_for_page = @page";

            var partnerContent = new PartnerContent();

            var res = DatabaseOperationProvider.Query(query, "content", new { lang = UrlLanguage.CurrentLanguage, domain = HttpContext.Current.Request.Url.Host, page = "" });
            
            if (res.Tables["content"].Rows.Count > 0)
                AddPartnerContent(ref partnerContent, res.Tables["content"].Rows[0]);

            res = DatabaseOperationProvider.Query(query, "content", new { lang = UrlLanguage.CurrentLanguage, domain = HttpContext.Current.Request.Url.Host, page = HttpContext.Current.Request.Url.AbsolutePath });

            if (res.Tables["content"].Rows.Count > 0)
                AddPartnerContent(ref partnerContent, res.Tables["content"].Rows[0]);

            return partnerContent;
        }
    }
}
