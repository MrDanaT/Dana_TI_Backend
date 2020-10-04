using System;
using System.Collections.Generic;
using System.Text;

namespace TennisClub.DTO.GameResult
{
    public class GameResultBaseDTO
    {
        public int GameId { get; set; }
        public byte SetNr { get; set; }
        public byte ScoreTeamMember { get; set; }
        public byte ScoreOpponent { get; set; }
    }
}
