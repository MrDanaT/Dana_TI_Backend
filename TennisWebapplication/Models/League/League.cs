using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisWebapplication.Models
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