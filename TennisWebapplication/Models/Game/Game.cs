using System;
using System.Collections;
using System.Collections.Generic;

namespace TennisWebapplication.Models
{
    public class Game
    {
        public Game()
        {
            GameResults = new HashSet<GameResult>();
        }

        public int Id { get; set; }
        public string GameNumber { get; set; }
        public int MemberId { get; set; }
        public int LeagueId { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<GameResult> GameResults { get; set; }
        public virtual Member MemberReference { get; set; }
        public virtual League LeagueReference { get; set; }
    }
}