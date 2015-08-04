namespace GuestService.Models
{
    using Sm.System.Mvc.Language;
    using System;
    using System.Runtime.CompilerServices;

    public class BaseApiParam
    {
        public string Language
        {
            get
            {
                return (!string.IsNullOrEmpty(this.ln) ? this.ln : UrlLanguage.CurrentLanguage);
            }
        }

        public string ln { get; set; }
    }
}

