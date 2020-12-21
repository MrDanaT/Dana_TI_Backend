using System;

namespace TennisClub.Common.Game
{
    public class GameReadDTO : BaseReadDTO
    {
        public string GameNumber { get; set; }
        public int MemberId { get; set; }
        public int LeagueId { get; set; }
        public DateTime Date { get; set; }
    }
}