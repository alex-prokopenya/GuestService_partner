namespace GuestService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class QuestionnaireGroup
    {
        public QuestionnaireGroup()
        {
            this.Questions = new List<QuestionnaireQuestion>();
        }

        public string Caption { get; set; }

        public int Id { get; set; }

        public List<QuestionnaireQuestion> Questions { get; private set; }
    }
}

