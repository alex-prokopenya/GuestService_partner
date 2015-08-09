namespace GuestService.Models.Payment
{
    using System;
    using System.Runtime.CompilerServices;

    public class ProcessingResultModel
    {
        public string invoice { get; set; }

        public string payerID { get; set; }

        public bool? success { get; set; }

        public string token { get; set; }

        
        public string order
        {
            get;
            set;
        }

        public string result
        {
            get;
            set;
        }

        public string payrefno
        {
            get;
            set;
        }
    }
}

