using System.Collections.Generic;

namespace TennisClub.BL.Entities
{
    public class League
    {
        public League()
        {
            Games = new HashSet<Game>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}