using System.Collections.Generic;
using TennisClub.BL.Entities.Common;

namespace TennisClub.BL.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            MemberRoles = new HashSet<MemberRole>();
        }
        public string Name { get; set; }

        public virtual ICollection<MemberRole> MemberRoles { get; set; }
    }
}