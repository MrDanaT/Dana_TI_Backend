using System;

namespace TennisClub.Common.Game
{
    public class GameCreateDTO
    {
        public string GameNumber { get; set; }
        public int MemberId { get; set; }
        public byte LeagueId { get; set; }
        public DateTime Date { get; set; }
    }
}
