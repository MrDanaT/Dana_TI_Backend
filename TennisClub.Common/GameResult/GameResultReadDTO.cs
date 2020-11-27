namespace TennisClub.Common.GameResult
{
    public class GameResultReadDTO : BaseReadDTO
    {
        public int GameId { get; set; }
        public byte SetNr { get; set; }
        public byte ScoreTeamMember { get; set; }
        public byte ScoreOpponent { get; set; }
    }
}
