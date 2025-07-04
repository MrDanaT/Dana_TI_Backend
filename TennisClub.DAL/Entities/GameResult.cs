﻿namespace TennisClub.DAL.Entities
{
    public class GameResult
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public byte SetNr { get; set; }
        public byte ScoreTeamMember { get; set; }
        public byte ScoreOpponent { get; set; }
        public Game GameNavigation { get; set; }
    }
}