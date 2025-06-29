﻿using System;
using System.Collections.Generic;

namespace TennisClub.DAL.Entities
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

        public ICollection<GameResult> GameResults { get; set; }
        public Member MemberNavigation { get; set; }
        public League LeagueNavigation { get; set; }
    }
}