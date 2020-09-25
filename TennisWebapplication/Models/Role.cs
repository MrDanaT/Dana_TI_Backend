using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisWebapplication.Models
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