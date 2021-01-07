namespace TennisClub.Common.League
{
    public class LeagueReadDTO : BaseReadDTO
    {
        public string Name { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}