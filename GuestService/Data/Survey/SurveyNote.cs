namespace GuestService.Data.Survey
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class SurveyNote
    {
        public DateTime CompleteDate { get; set; }

        public int Invitation { get; set; }

        public string Language { get; set; }

        public List<SurveyNoteItem> Notes { get; set; }

        public string ParticipantName { get; set; }

        public string ParticipantPrefix { get; set; }
    }
}

