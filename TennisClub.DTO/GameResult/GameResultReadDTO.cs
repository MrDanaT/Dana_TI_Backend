namespace TennisClub.DTO.GameResult
{
    public class GameResultReadDTO : GameResultBaseDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public BL.Entities.Game GameNavigation { get; set; }
    }
}
