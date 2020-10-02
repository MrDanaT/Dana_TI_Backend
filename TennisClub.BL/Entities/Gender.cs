using System.Collections.Generic;
using TennisClub.BL.Entities.Common;

namespace TennisClub.BL.Entities
{
    public class Gender : BaseEntity
    {
        public Gender()
        {
            Members = new HashSet<Member>();
        }


        public string Name { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}