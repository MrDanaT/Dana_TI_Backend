using System.Collections.Generic;

namespace TennisClub.BL.Entities
{
    public class Gender
    {
        public Gender()
        {
            Members = new HashSet<Member>();
        }
        public byte Id { get; set; }
        public string Name { get; set; }

        public ICollection<Member> Members { get; set; }
    }
}