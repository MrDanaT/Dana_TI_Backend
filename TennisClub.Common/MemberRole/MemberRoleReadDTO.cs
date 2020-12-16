using System;

namespace TennisClub.Common.MemberRole
{
    public class MemberRoleReadDTO : BaseReadDTO
    {
        public int MemberId { get; set; }
        public string MemberFullName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
