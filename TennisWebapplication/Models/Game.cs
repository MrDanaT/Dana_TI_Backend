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
        public virtual Member MemberId { get; set; }
        public virtual League LeagueId { get; set; }
        public DateTime Date { get; set; }

        public ICollection<GameResult> GameResults { get; set; }
    }
}