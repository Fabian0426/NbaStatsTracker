using System.Globalization;

namespace NbaStatsTrackerBackend.Application.Errors
{
    public class Errors
    {
        public const string TeamsNotFound = "Error finding teams.";
        public const string SpecificTeamNotFound = "The specified team was not found.";

        public const string PlayersNotFound = "Error finding players.";
        public const string SpecificPlayerNotFound = "The specified player was not found.";

        public const string GamesNotFound = "Error finding games.";
    }
}
