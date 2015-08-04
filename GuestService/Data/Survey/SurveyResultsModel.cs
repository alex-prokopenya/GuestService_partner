namespace GuestService.Data.Survey
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SurveyResultsModel
    {
        public SurveyResultsModel()
        {
            this.questions = new Dictionary<string, SurveyQuestionModel>();
        }

        public string accesscode { get; set; }

        public SurveyCustomerModel guest { get; set; }

        public Dictionary<string, SurveyQuestionModel> questions { get; private set; }
    }
}

