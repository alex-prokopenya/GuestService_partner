namespace GuestService.Code.Payment
{
    using System;
    using System.Configuration;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;

    public class UnitellerPaymentResult
    {
        public static UnitellerPaymentResult Create(HttpRequestBase request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            string str = request["Order_ID"];
            if (string.IsNullOrEmpty(str))
            {
                throw new Exception("Order_ID is empty");
            }
            string str2 = request["Status"];
            if (string.IsNullOrEmpty(str2))
            {
                throw new Exception("Status is empty");
            }
            string str3 = request["Signature"];
            if (string.IsNullOrEmpty(str3))
            {
                throw new Exception("Signature is empty");
            }
            StringBuilder builder = new StringBuilder();
            using (MD5 md = MD5.Create())
            {
                string s = string.Format("{0}{1}{2}", str, str2, ConfigurationManager.AppSettings["unitellerPassword"]);
                foreach (byte num in md.ComputeHash(Encoding.UTF8.GetBytes(s)))
                {
                    builder.AppendFormat("{0:X2}", num);
                }
            }
            if (builder.ToString() != str3)
            {
                throw new Exception(string.Format("invalid signature", new object[0]));
            }
            return new UnitellerPaymentResult { InvoiceNumber = str, Status = ((str2.ToLower() == "authorized") || (str2.ToLower() == "paid")) ? OpeationStatus.Success : OpeationStatus.Failed };
        }

        public string InvoiceNumber { get; private set; }

        public OpeationStatus Status { get; private set; }

        public enum OpeationStatus
        {
            Failed,
            Success
        }
    }
}

