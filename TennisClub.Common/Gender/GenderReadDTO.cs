﻿namespace TennisClub.Common.Gender
{
    public class GenderReadDTO : BaseReadDTO
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}