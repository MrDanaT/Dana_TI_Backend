using System;
using System.Collections.Generic;
using System.Text;

namespace TennisClub.DTO.GameResult
{
    public class GameResultBaseDTO
    {
        public byte SetNr { get; set; }
        public byte ScoreTeamMember { get; set; }
        public byte ScoreOpponent { get; set; }
    }
}
