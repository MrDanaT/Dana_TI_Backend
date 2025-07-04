﻿using System.Collections.Generic;

namespace TennisClub.DAL.Entities
{
    public class Gender
    {
        public Gender()
        {
            Members = new HashSet<Member>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}