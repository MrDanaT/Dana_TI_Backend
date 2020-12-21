using System;

namespace TennisClub.Common.Role
{
    public class RoleReadDTO : BaseReadDTO
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is RoleReadDTO dTO &&
                   Name == dTO.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}