using TennisClub.BL.Entities.Common;

namespace TennisClub.BL.Entities
{
    public class GameResult : BaseEntity
    {
        public int GameId { get; set; }
        public byte SetNr { get; set; }
        public byte ScoreTeamMember { get; set; }
        public byte ScoreOpponent { get; set; }
        public virtual Game GameNavigation { get; set; }

    }
}