namespace TennisClub.DTO.Member
{
    public class MemberReadDTO : MemberBaseDTO
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}
