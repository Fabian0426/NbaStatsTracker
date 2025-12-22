namespace NbaStatsTrackerBackend.Domain.Entities
{
    public sealed class NbaGames
    {
        public int Id { get; }
        public DateTime Date { get; }
        public int Season { get; }
        public string? Status { get; }
        public int? Period { get; }
        public string? Time { get; }
        public bool? Postseason { get; }
        public int? HomeTeamScore { get; }
        public int? VisitorTeamScore { get; }
        public DateTime Datetime { get; }
        public int? HomeQ1 { get; }
        public int? HomeQ2 { get; }
        public int? HomeQ3 { get; }
        public int? HomeQ4 { get; }
        public int? HomeOt1 { get; }
        public int? HomeOt2 { get; }
        public int? HomeOt3 { get; }
        public int? HomeTimeoutsRemaining { get; }
        public bool? HomeInBonus { get; }
        public int? VisitorQ1 { get; }
        public int? VisitorQ2 { get; }
        public int? VisitorQ3 { get; }
        public int? VisitorQ4 { get; }
        public int? VisitorOt1 { get; }
        public int? VisitorOt2 { get; }
        public int? VisitorOt3 { get; }
        public int? VisitorTimeoutsRemaining { get; }
        public bool? VisitorInBonus { get; }
        public NbaTeams HomeTeam { get; }
        public NbaTeams VisitorTeam { get; }

        public NbaGames(
            int id,
            DateTime date,
            int season,
            string? status,
            int? period,
            string? time,
            bool? postseason,
            int? homeTeamScore,
            int? visitorTeamScore,
            DateTime datetime,
            int? homeQ1,
            int? homeQ2,
            int? homeQ3,
            int? homeQ4,
            int? homeOt1,
            int? homeOt2,
            int? homeOt3,
            int? homeTimeoutsRemaining,
            bool? homeInBonus,
            int? visitorQ1,
            int? visitorQ2,
            int? visitorQ3,
            int? visitorQ4,
            int? visitorOt1,
            int? visitorOt2,
            int? visitorOt3,
            int? visitorTimeoutsRemaining,
            bool? visitorInBonus,
            NbaTeams homeTeam,
            NbaTeams visitorTeam)
        {
            Id = id;
            Date = date;
            Season = season;
            Status = status;
            Period = period;
            Time = time;
            Postseason = postseason;
            HomeTeamScore = homeTeamScore;
            VisitorTeamScore = visitorTeamScore;
            Datetime = datetime;
            HomeQ1 = homeQ1;
            HomeQ2 = homeQ2;
            HomeQ3 = homeQ3;
            HomeQ4 = homeQ4;
            HomeOt1 = homeOt1;
            HomeOt2 = homeOt2;
            HomeOt3 = homeOt3;
            HomeTimeoutsRemaining = homeTimeoutsRemaining;
            HomeInBonus = homeInBonus;
            VisitorQ1 = visitorQ1;
            VisitorQ2 = visitorQ2;
            VisitorQ3 = visitorQ3;
            VisitorQ4 = visitorQ4;
            VisitorOt1 = visitorOt1;
            VisitorOt2 = visitorOt2;
            VisitorOt3 = visitorOt3;
            VisitorTimeoutsRemaining = visitorTimeoutsRemaining;
            VisitorInBonus = visitorInBonus;
            HomeTeam = homeTeam;
            VisitorTeam = visitorTeam;
        }
    }
}