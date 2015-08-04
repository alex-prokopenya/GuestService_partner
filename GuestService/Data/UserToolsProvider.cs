namespace GuestService.Data
{
    using GuestService;
    using Sm.System.Database;
    using Sm.System.Exceptions;
    using System;
    using System.Data;
    using System.Linq;

    public static class UserToolsProvider
    {
        private static UserToolsFactory factory = new UserToolsFactory();

        public static WebPartner FindPartnerByOnlineSID(string sid)
        {
            if (string.IsNullOrEmpty(sid))
            {
                throw new ArgumentNullExceptionWithCode(215, "sid");
            }
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_findPartnerByOnlineSID", "partner", new
            {
                sid
            });
            return (
                from DataRow row in ds.Tables["partner"].Rows
                select UserToolsProvider.factory.OnlinePartner(row)).FirstOrDefault<WebPartner>();
        }

        public static WebPartner FindPartnerForPublicWeb(string alias)
        {
            if (string.IsNullOrEmpty(alias))
            {
                throw new ArgumentNullExceptionWithCode(214, "alias");
            }
            DataSet ds = DatabaseOperationProvider.QueryProcedure("up_getPartnerForPublicWeb", "partner", new
            {
                alias
            });
            WebPartner result = (
                from DataRow row in ds.Tables["partner"].Rows
                select UserToolsProvider.factory.WebPartner(row)).FirstOrDefault<WebPartner>();
            if (result != null)
            {
                result.alias = alias;
            }
            return result;
        }

        public static WebPartner GetPartner(IPartnerParam param)
        {
            WebPartner partner;
            if (param == null)
            {
                throw new ArgumentNullException("param");
            }
            if (!string.IsNullOrEmpty(param.PartnerSessionID))
            {
                partner = FindPartnerByOnlineSID(param.PartnerSessionID);
                if (partner == null)
                {
                    throw new ExceptionWithCode(0xd5, string.Format("invalid sid '{0}'", param.PartnerSessionID));
                }
                return partner;
            }
            if (!string.IsNullOrEmpty(param.PartnerAlias))
            {
                partner = FindPartnerForPublicWeb(param.PartnerAlias);
                if (partner == null)
                {
                    throw new ArgumentExceptionWithCode(0xd4, string.Format("invalid partner alias '{0}'", param.PartnerAlias));
                }
                return partner;
            }
            string excursionDefaultPartnerAlias = Settings.ExcursionDefaultPartnerAlias;
            if (string.IsNullOrEmpty(excursionDefaultPartnerAlias))
            {
                throw new ExceptionWithCode(210, "partner authentication required: use 'pa' or 'psid' params");
            }
            partner = FindPartnerForPublicWeb(excursionDefaultPartnerAlias);
            if (partner == null)
            {
                throw new ArgumentExceptionWithCode(0xd3, string.Format("invalid default partner alias '{0}'", excursionDefaultPartnerAlias));
            }
            return partner;
        }

        public static void UmgRaiseMessage(string lang, string title, string address, string template, string data)
        {
            DatabaseOperationProvider.ExecuteProcedure("umg.up_RaiseMessage", new { type = "MAIL", address = address, lang = lang, title = title, template = template, addresseedata = data });
        }

        private class UserToolsFactory
        {
            internal GuestService.Data.WebPartner OnlinePartner(DataRow row)
            {
                return new GuestService.Data.WebPartner { id = row.ReadInt("partner"), passId = row.ReadInt("partpass"), alias = row.ReadNullableTrimmedString("alias") };
            }

            internal GuestService.Data.WebPartner WebPartner(DataRow row)
            {
                return new GuestService.Data.WebPartner { id = row.ReadInt("partner"), passId = row.ReadInt("partpass") };
            }
        }
    }
}

