using System.Collections.Generic;

namespace TennisClub.DAL.Entities
{
    public class Role
    {
        public Role()
        {
            MemberRoles = new HashSet<MemberRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<MemberRole> MemberRoles { get; set; }
    }
}