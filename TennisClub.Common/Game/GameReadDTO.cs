using System;

namespace TennisClub.Common.Game
{
    public class GameReadDTO : BaseReadDTO
    {
        public string GameNumber { get; set; }
        public int MemberId { get; set; }
        public string MemberFullName { get; set; }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return GameNumber;
        }
    }
}