using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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