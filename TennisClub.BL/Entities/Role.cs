using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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