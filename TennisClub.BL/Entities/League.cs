using System.Collections.Generic;
using TennisClub.BL.Entities.Common;

namespace TennisClub.BL.Entities
{
    public class League : BaseEntity
    {
        public League()
        {
            Games = new HashSet<Game>();
        }

        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
    }
}