﻿using TennisClub.Common.Game;

namespace TennisClub.Common.GameResult
{
    public class GameResultReadDTO : BaseReadDTO
    {
        public int GameId { get; set; }
        public byte SetNr { get; set; }
        public byte ScoreTeamMember { get; set; }
        public byte ScoreOpponent { get; set; }
        public GameReadDTO GameNavigation { get; set; }

        public override string ToString()
        {
            return GameId.ToString();
        }
    }
}