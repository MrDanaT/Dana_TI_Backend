using System.Collections.Generic;

namespace TennisClub.DAL.Entities
{
    public class League
    {
        public League()
        {
            Games = new HashSet<Game>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}