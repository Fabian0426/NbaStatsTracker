namespace NbaStatsTrackerBackend.Domain.Entities
{
    public sealed class Team
    {
        public int Id { get; }
        public string Conference { get; } = string.Empty;
        public string Division { get; } = string.Empty;
        public string City { get; } = string.Empty;
        public string Name { get; } = string.Empty;
        public string FullName { get; } = string.Empty;
        public string Abbreviation { get; } = string.Empty;

        public Team(
            int id,
            string conference,
            string division,
            string city,
            string name,
            string fullName,
            string abbreviation)
        {
            Id = id;
            Conference = conference;
            Division = division;
            City = city;
            Name = name;
            FullName = fullName;
            Abbreviation = abbreviation;
        }
    }
}