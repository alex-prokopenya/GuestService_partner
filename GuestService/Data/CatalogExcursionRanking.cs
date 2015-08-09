namespace GuestService.Data
{
    using GuestService.Data.Survey;
    using GuestService.Resources;
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml.Serialization;

    public class CatalogExcursionRanking
    {
        protected void BuildAverageTitle(string language)
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture(language);
            if (this.average >= 9.5M)
            {
                this.title = RankingTitleStrings.Get("Rank_1");
            }
            else if (this.average >= 9.0M)
            {
                this.title = RankingTitleStrings.Get("Rank_2");
            }
            else if (this.average >= 8.0M)
            {
                this.title = RankingTitleStrings.Get("Rank_3");
            }
            else if (this.average >= 7.0M)
            {
                this.title = RankingTitleStrings.Get("Rank_4");
            }
            else
            {
                this.title = string.Empty;
            }
        }

        public static CatalogExcursionRanking Create(ExcursionRank rank, string language)
        {
            if (rank == null)
            {
                throw new ArgumentNullException("rank");
            }
            CatalogExcursionRanking ranking = new CatalogExcursionRanking {
                count = rank.SurveyCount
            };
            decimal? averageRank = rank.AverageRank;
            ranking.average = averageRank.HasValue ? new decimal?(Math.Round(rank.AverageRank.Value, 1, MidpointRounding.AwayFromZero)) : null;
            ranking.BuildAverageTitle(language);
            return ranking;
        }

        [XmlAttribute("average")]
        public decimal? average { get; set; }

        [XmlElement("count")]
        public int count { get; set; }

        [XmlAttribute("title")]
        public string title { get; set; }
    }
}

