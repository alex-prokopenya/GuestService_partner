﻿namespace GuestService.Controllers.Html
{
    using GuestService;
    using GuestService.Code;
    using GuestService.Data;
    using GuestService.Models.Payment;
    using GuestService.Resources;
    using PayPal.PayPalAPIInterfaceService;
    using PayPal.PayPalAPIInterfaceService.Model;
    using Sm.Report;
    using Sm.System.Mvc.Language;
    using Sm.System.Trace;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    [HttpPreferences, UrlLanguage, WebSecurityInitializer]
    public class PaymentController : BaseController
    {
        [HttpPost, ValidateAntiForgeryToken, ActionName("index")]
        public ActionResult Index(PaymentModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            if (model.claimId == 0)
            {
                throw new ArgumentException("claimid");
            }
            PaymentContext context = new PaymentContext {
                Reservation = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, model.claimId),
                PaymentModes = BookingProvider.GetPaymentModes(UrlLanguage.CurrentLanguage, model.claimId)
            };
            return base.View(context);
        }

        [HttpGet, ActionName("index")]
        public ActionResult Index(int? claim)
        {
            PaymentContext model = new PaymentContext();
            if (claim.HasValue)
            {
                model.Reservation = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, claim.Value);
                model.PaymentModes = BookingProvider.GetPaymentModes(UrlLanguage.CurrentLanguage, claim.Value);
            }
            return base.View(model);
        }

        private static Dictionary<string, string> PayPal_CreateConfig()
        {
            PaymentPaypalSettings paymentPaypal = Settings.PaymentPaypal;
            if (paymentPaypal == null)
            {
                throw new Exception("PayPal settings are not configured");
            }
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary.Add("mode", paymentPaypal.IsSandbox ? "sandbox" : "live");
            dictionary.Add("account1.apiUsername", paymentPaypal.Username);
            dictionary.Add("account1.apiPassword", paymentPaypal.Password);
            dictionary.Add("account1.apiSignature", paymentPaypal.Signature);
            return dictionary;
        }

        [ValidateAntiForgeryToken, ActionName("processing"), HttpPost]
        public ActionResult Processing(ProcessingModel model)
        {
            if (model == null)
            {
                throw new System.ArgumentNullException("model");
            }
            if (model.claimId == 0)
            {
                throw new System.ArgumentException("claimid");
            }
            PaymentMode paymentMode = (
                from m in BookingProvider.GetPaymentModes(UrlLanguage.CurrentLanguage, model.claimId)
                where m.id == model.paymentId
                select m).FirstOrDefault<PaymentMode>();
            if (paymentMode == null)
            {
                throw new System.Exception(string.Format("payment mode id '{0}' not found", model.paymentId));
            }
            string text = (paymentMode.processing ?? "").ToLowerInvariant();
            if (text != null)
            {
                ActionResult result;
                if (!(text == "paypal"))
                {
                    if (!(text == "uniteller"))
                    {
                        if (!(text == "bank"))
                        {
                            goto IL_120;
                        }
                        result = this.Processing_Bank(model.claimId, paymentMode);
                    }
                    else
                    {
                        result = this.Processing_Uniteller(model.claimId, paymentMode);
                    }
                }
                else
                {
                    result = this.Processing_PayPal(model.claimId, paymentMode);
                }
                return result;
            }
            IL_120:
            throw new System.Exception(string.Format("unsupported processing system '{0}'", paymentMode.processing));
        }

        private ActionResult Processing_Bank(int claim, PaymentMode payment)
        {
            ActionResult result4;
            if (payment == null)
            {
                throw new ArgumentNullException("payment");
            }
            PaymentBeforeProcessingResult result = BookingProvider.BeforePaymentProcessing(UrlLanguage.CurrentLanguage, payment.paymentparam);
            if (result == null)
            {
                throw new Exception("cannot get payment details");
            }
            if (!result.success)
            {
                throw new Exception("payment details fail");
            }
            try
            {
                List<ReportParam> list = new List<ReportParam>();
                ReportParam item = new ReportParam {
                    Name = "vClaimList",
                    Value = claim.ToString()
                };
                list.Add(item);
                string str = ConfigurationManager.AppSettings["report_PrintInvoice"];
                if (string.IsNullOrEmpty(str))
                {
                    throw new Exception("report_PrintInvoice is empty");
                }
                ReportResult result2 = ReportServer.BuildReport(str, ReportFormat.pdf, list.ToArray());
                if (result2 == null)
                {
                    throw new Exception("report data is empty");
                }
                MemoryStream fileStream = new MemoryStream(result2.Content);
                FileStreamResult result3 = new FileStreamResult(fileStream, "application/pdf") {
                    FileDownloadName = string.Format("invoice_{0}.pdf", claim)
                };
                result4 = result3;
            }
            catch (Exception exception)
            {
                Tracing.ServiceTrace.TraceEvent(TraceEventType.Error, 0, exception.ToString());
                throw;
            }
            return result4;
        }

        private ActionResult Processing_PayPal(int claim, PaymentMode payment)
        {
            if (payment == null)
            {
                throw new System.ArgumentNullException("payment");
            }
            PaymentBeforeProcessingResult beforePaymentResult = BookingProvider.BeforePaymentProcessing(UrlLanguage.CurrentLanguage, payment.paymentparam);
            if (beforePaymentResult == null)
            {
                throw new System.Exception("cannot get payment details");
            }
            if (!beforePaymentResult.success)
            {
                throw new System.Exception("payment details fail");
            }
            System.Collections.Generic.List<PaymentDetailsType> paymentDetails = new System.Collections.Generic.List<PaymentDetailsType>();
            PaymentDetailsType paymentDetail = new PaymentDetailsType();
            paymentDetail.AllowedPaymentMethod = new AllowedPaymentMethodType?(AllowedPaymentMethodType.ANYFUNDINGSOURCE);
            CurrencyCodeType currency = (CurrencyCodeType)EnumUtils.GetValue(payment.payrest.currency, typeof(CurrencyCodeType));
            PaymentDetailsItemType paymentItem = new PaymentDetailsItemType();
            paymentItem.Name = string.Format(PaymentStrings.ResourceManager.Get("PaymentForOrderFormat"), claim);
            paymentItem.Amount = new BasicAmountType(new CurrencyCodeType?(currency), payment.payrest.total.ToString("#.00", System.Globalization.NumberFormatInfo.InvariantInfo));
            paymentItem.Quantity = new int?(1);
            paymentItem.ItemCategory = new ItemCategoryType?(ItemCategoryType.PHYSICAL);
            paymentItem.Description = string.Format("Booking #{0}", claim);
            paymentDetail.PaymentDetailsItem = new System.Collections.Generic.List<PaymentDetailsItemType>
            {
                paymentItem
            };
            paymentDetail.PaymentAction = new PaymentActionCodeType?(PaymentActionCodeType.SALE);
            paymentDetail.OrderTotal = new BasicAmountType(paymentItem.Amount.currencyID, paymentItem.Amount.value);
            paymentDetails.Add(paymentDetail);
            SetExpressCheckoutRequestDetailsType ecDetails = new SetExpressCheckoutRequestDetailsType();
            ecDetails.ReturnURL = new Uri(base.Request.BaseServerAddress(), base.Url.Action("processingresult", new
            {
                id = "paypal",
                success = true
            })).ToString();
            ecDetails.CancelURL = new Uri(base.Request.BaseServerAddress(), base.Url.Action("processingresult", new
            {
                id = "paypal",
                success = false
            })).ToString();
            ecDetails.NoShipping = "1";
            ecDetails.AllowNote = "0";
            ecDetails.SolutionType = new SolutionTypeType?(SolutionTypeType.SOLE);
            ecDetails.SurveyEnable = "0";
            ecDetails.PaymentDetails = paymentDetails;
            ecDetails.InvoiceID = beforePaymentResult.invoiceNumber;
            SetExpressCheckoutRequestType request = new SetExpressCheckoutRequestType();
            request.Version = "104.0";
            request.SetExpressCheckoutRequestDetails = ecDetails;
            SetExpressCheckoutReq wrapper = new SetExpressCheckoutReq();
            wrapper.SetExpressCheckoutRequest = request;
            System.Collections.Generic.Dictionary<string, string> config = PaymentController.PayPal_CreateConfig();
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(config);
            SetExpressCheckoutResponseType setECResponse = service.SetExpressCheckout(wrapper);
            System.Collections.Generic.KeyValuePair<string, string> sandboxConfig = config.FirstOrDefault((System.Collections.Generic.KeyValuePair<string, string> m) => m.Key == "mode");
            string sandboxServer = (sandboxConfig.Key != null && sandboxConfig.Value == "sandbox") ? ".sandbox" : "";
            return new RedirectResult(string.Format("https://www{0}.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token={1}", sandboxServer, base.Server.UrlEncode(setECResponse.Token)));
        }

        private ActionResult Processing_Uniteller(int claim, PaymentMode payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException("payment");
            }
            PaymentBeforeProcessingResult result = BookingProvider.BeforePaymentProcessing(UrlLanguage.CurrentLanguage, payment.paymentparam);
            if (result == null)
            {
                throw new Exception("cannot get payment details");
            }
            if (!result.success)
            {
                throw new Exception("payment details fail");
            }
            ProcessingContext model = new ProcessingContext {
                Reservation = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, claim),
                PaymentMode = payment,
                BeforePaymentResult = result
            };
            return base.View(@"PaymentSystems\Uniteller", model);
        }

        [ActionName("processingresult"), HttpGet]
        public ActionResult ProcessingResult(string id, ProcessingResultModel model)
        {
            switch ((id ?? "").ToLowerInvariant())
            {
                case "paypal":
                    return this.ProcessingResult_PayPal(model);

                case "uniteller":
                    return this.ProcessingResult_Uniteller(model);
            }
            throw new Exception(string.Format("unsupported processing system '{0}'", id));
        }

        private ActionResult ProcessingResult_PayPal(ProcessingResultModel model)
        {
            if (model == null)
            {
                throw new System.ArgumentNullException("model");
            }
            PaymentResultContext context = new PaymentResultContext();
            if (model.success == true)
            {
                if (model.token == null)
                {
                    throw new System.ArgumentNullException("token");
                }
                if (model.payerID == null)
                {
                    throw new System.ArgumentNullException("payerID");
                }
                GetExpressCheckoutDetailsRequestType request = new GetExpressCheckoutDetailsRequestType();
                request.Version = "104.0";
                request.Token = model.token;
                GetExpressCheckoutDetailsReq wrapper = new GetExpressCheckoutDetailsReq();
                wrapper.GetExpressCheckoutDetailsRequest = request;
                System.Collections.Generic.Dictionary<string, string> config = PaymentController.PayPal_CreateConfig();
                PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(config);
                GetExpressCheckoutDetailsResponseType ecResponse = service.GetExpressCheckoutDetails(wrapper);
                if (ecResponse == null)
                {
                    throw new System.Exception("checkout details result is null");
                }
                if (ecResponse.Errors != null && ecResponse.Errors.Count > 0)
                {
                    ecResponse.Errors.ForEach(delegate (ErrorType m)
                    {
                        context.Errors.Add(m.LongMessage);
                    });
                }
                if (ecResponse.Ack == AckCodeType.SUCCESS || ecResponse.Ack == AckCodeType.SUCCESSWITHWARNING)
                {
                    GetExpressCheckoutDetailsResponseDetailsType details = ecResponse.GetExpressCheckoutDetailsResponseDetails;
                    if (details == null)
                    {
                        throw new System.Exception("details object is null");
                    }
                    if (string.IsNullOrEmpty(details.InvoiceID))
                    {
                        throw new System.Exception("invoiceID not found");
                    }
                    if (details.PaymentDetails == null)
                    {
                        throw new System.Exception("payment details is null");
                    }
                    System.Collections.Generic.List<PaymentDetailsType> paymentDetails = new System.Collections.Generic.List<PaymentDetailsType>();
                    foreach (PaymentDetailsType payment in details.PaymentDetails)
                    {
                        paymentDetails.Add(new PaymentDetailsType
                        {
                            NotifyURL = null,
                            PaymentAction = payment.PaymentAction,
                            OrderTotal = payment.OrderTotal
                        });
                    }
                    DoExpressCheckoutPaymentRequestType paymentRequest = new DoExpressCheckoutPaymentRequestType();
                    paymentRequest.Version = "104.0";
                    paymentRequest.DoExpressCheckoutPaymentRequestDetails = new DoExpressCheckoutPaymentRequestDetailsType
                    {
                        PaymentDetails = paymentDetails,
                        Token = model.token,
                        PayerID = model.payerID
                    };
                    DoExpressCheckoutPaymentResponseType doECResponse = service.DoExpressCheckoutPayment(new DoExpressCheckoutPaymentReq
                    {
                        DoExpressCheckoutPaymentRequest = paymentRequest
                    });
                    if (doECResponse == null)
                    {
                        throw new System.Exception("payment result is null");
                    }
                    if (doECResponse.Errors != null && doECResponse.Errors.Count > 0)
                    {
                        doECResponse.Errors.ForEach(delegate (ErrorType m)
                        {
                            context.Errors.Add(m.LongMessage);
                        });
                    }
                    if (doECResponse.Ack == AckCodeType.SUCCESS || doECResponse.Ack == AckCodeType.SUCCESSWITHWARNING)
                    {
                        ConfirmInvoiceResult invoiceResult = BookingProvider.ConfirmInvoice(details.InvoiceID.Trim());
                        Tracing.DataTrace.TraceEvent(TraceEventType.Information, 0, "PAYPAL transaction: invoice: '{0}', invoice confirmation: '{1}'", new object[]
                        {
                            details.InvoiceID,
                            invoiceResult.IsSuccess ? "SUCCESS" : "FAILED"
                        });
                        if (!invoiceResult.IsSuccess)
                        {
                            context.Errors.Add(string.Format("invoice confirmation error: {0}", invoiceResult.ErrorMessage));
                        }
                        else
                        {
                            context.Success = true;
                        }
                    }
                }
            }
            else
            {
                context.Errors.Add(PaymentStrings.PaymentCancelled);
            }
            return base.View("_ProcessingResult", context);
        }

        private ActionResult ProcessingResult_Uniteller(ProcessingResultModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException("model");
            }
            PaymentResultContext context = new PaymentResultContext();
            if (model.success == true)
            {
                context.Success = true;
            }
            else
            {
                context.Errors.Add(PaymentStrings.PaymentCancelled);
            }
            return base.View("_ProcessingResult", context);
        }
    }
}
