namespace GuestService.Code
{
    using GuestService.Data;
    using System;
    using System.Runtime.CompilerServices;

    public static class QuestionnaireExtension
    {
        public static bool ContainsIssues(this QuestionnaireQuestion question)
        {
            return (question.Issues.Count > 0);
        }

        public static bool ContainsQuestions(this QuestionnaireGroup group)
        {
            foreach (QuestionnaireQuestion question in group.Questions)
            {
                if (question.ContainsIssues() || question.IsNote)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

