namespace TennisClub.DTO.GameResult
{
    public class GameResultReadDTO : GameResultBaseDTO
    {
        public BL.Entities.Game GameNavigation { get; set; }
    }
}
