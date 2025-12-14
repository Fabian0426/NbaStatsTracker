namespace NbaStatsTrackerBackend.Domain.Entities
{
    public sealed class NbaPlayers
    {
        public int Id { get; }
        public string FirstName { get; } = string.Empty;
        public string LastName { get; } = string.Empty;
        public string Position { get; } = string.Empty;
        public string? Height { get; }  
        public string? Weight { get; }
        public string? JerseyNumber { get; }
        public string? College { get; }
        public string? Country { get; }
        public int? DraftYear { get; }
        public int? DraftRound { get; }
        public int? DraftNumber { get; }
        public NbaTeams Team { get; }

        public NbaPlayers(
            int id,
            string firstName,
            string lastName,
            string position,
            string? height,
            string? weight,
            string? jerseyNumber,
            string? college,
            string? country,
            int? draftYear,
            int? draftRound,
            int? draftNumber,
            NbaTeams team)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Height = height;
            Weight = weight;
            JerseyNumber = jerseyNumber;
            College = college;
            Country = country;
            DraftYear = draftYear;
            DraftRound = draftRound;
            DraftNumber = draftNumber;
            Team = team;
        }
    }
}