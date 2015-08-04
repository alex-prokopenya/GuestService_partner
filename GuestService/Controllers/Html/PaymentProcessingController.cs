namespace GuestService.Controllers.Html
{
    using GuestService.Code;
    using GuestService.Code.Payment;
    using GuestService.Data;
    using Sm.System.Trace;
    using System;
    using System.Configuration;
    using System.Diagnostics;
    using System.Web.Mvc;

    [OutputCache(NoStore=true, Duration=0, VaryByParam="*")]
    public class PaymentProcessingController : Controller
    {
        private void CheckServerAddressList()
        {
            string str = ConfigurationManager.AppSettings["paymentSystemIP"];
            if (!string.IsNullOrEmpty(str))
            {
                string[] strArray = str.Split(";,".ToCharArray());
                string str2 = base.HttpContext.Request.ServerVariables["REMOTE_ADDR"];
                if (string.IsNullOrEmpty(str2))
                {
                    throw new Exception("'REMOTE_ADDR' variable not found");
                }
                foreach (string str3 in strArray)
                {
                    if (str3 == str2)
                    {
                        return;
                    }
                }
                throw new Exception("invalid server address");
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult uniteller()
        {
            string requiredString = base.RouteData.GetRequiredString("action");
            try
            {
                Tracing.DataTrace.TraceEvent(TraceEventType.Verbose, 0, "UNITELLER payment data: {0}", new object[] { base.Request.DumpValues() });
                this.CheckServerAddressList();
                UnitellerPaymentResult result = UnitellerPaymentResult.Create(base.Request);
                if (result.Status == UnitellerPaymentResult.OpeationStatus.Success)
                {
                    ConfirmInvoiceResult result2 = BookingProvider.ConfirmInvoice(result.InvoiceNumber.Trim());
                    Tracing.DataTrace.TraceEvent(TraceEventType.Information, 0, "UNITELLER transaction: invoice: '{0}', status: '{1}', invoice confirmation: '{2}'", new object[] { result.InvoiceNumber, result.Status, result2.IsSuccess ? "SUCCESS" : "FAILED" });
                    if (!result2.IsSuccess)
                    {
                        throw new Exception(string.Format("invoice confirm error {0}", result2.ErrorMessage));
                    }
                }
                else
                {
                    Tracing.DataTrace.TraceEvent(TraceEventType.Information, 0, "UNITELLER transaction: invoice: '{1}', status: '{2}'", new object[] { result.InvoiceNumber, result.Status });
                }
                return new EmptyResult();
            }
            catch (Exception exception)
            {
                Tracing.DataTrace.TraceEvent(TraceEventType.Error, 0, "UNITELLER payment error: {0}", new object[] { exception.ToString() });
                return new HttpStatusCodeResult(500);
            }
        }
    }
}

