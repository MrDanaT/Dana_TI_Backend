using System;
using TennisClub.Common.Game;

namespace TennisClub.Common.GameResult
{
    public class GameResultReadDTO : BaseReadDTO
    {
        public int GameId { get; set; }
        public byte SetNr { get; set; }
        public byte ScoreTeamMember { get; set; }
        public byte ScoreOpponent { get; set; }
        public GameReadDTO GameNavigation { get; set; }

        public override bool Equals(object obj)
        {
            return obj is GameResultReadDTO dTO &&
                   SetNr == dTO.SetNr &&
                   ScoreTeamMember == dTO.ScoreTeamMember &&
                   ScoreOpponent == dTO.ScoreOpponent;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SetNr, ScoreTeamMember, ScoreOpponent);
        }
    }
}