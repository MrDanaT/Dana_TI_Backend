﻿namespace TennisClub.Common.Role
{
    public class RoleReadDTO : BaseReadDTO
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}