namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class QuestionnaireQuestion
    {
        public QuestionnaireQuestion()
        {
            this.Issues = new List<QuestionnaireIssue>();
        }

        public string Category { get; set; }

        public int Id { get; set; }

        public bool IsMultiple { get; set; }

        public bool IsNote { get; set; }

        public List<QuestionnaireIssue> Issues { get; private set; }

        public string NoteCaption { get; set; }

        public string Text { get; set; }
    }
}

