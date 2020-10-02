using System.Collections.Generic;

namespace TennisClub.BL.Entities
{
    public class Role
    {
        public Role()
        {
            MemberRoles = new HashSet<MemberRole>();
        }
        public byte Id { get; set; }
        public string Name { get; set; }

        public ICollection<MemberRole> MemberRoles { get; set; }
    }
}