using System;
using System.Collections;
using System.Collections.Generic;

namespace TennisClub.BL.Entities
{
    public class Game : BaseEntity
    {
        public Game()
        {
            GameResults = new HashSet<GameResult>();
        }

        public string GameNumber { get; set; }
        public int MemberId { get; set; }
        public byte LeagueId { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<GameResult> GameResults { get; set; }
        public virtual Member MemberNavigation { get; set; }
        public virtual League LeagueNavigation { get; set; }
    }
}