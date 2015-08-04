namespace GuestService.Data.Survey
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ExcursionRankInfo : ExcursionRank
    {
        public List<CharacteristicRank> Characteristics { get; set; }
    }
}

