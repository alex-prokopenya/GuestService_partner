namespace GuestService.Data.Survey
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SurveyQuestionModel
    {
        public string issue { get; set; }

        public Dictionary<string, string> marks { get; set; }

        public string note { get; set; }
    }
}

