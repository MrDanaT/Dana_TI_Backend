using System;

namespace TennisClub.Common.MemberRole
{
    public class MemberRoleCreateDTO
    {
        public int MemberId { get; set; }
        public int RoleId { get; set; }
        public DateTime StartDate { get; set; }
    }
}