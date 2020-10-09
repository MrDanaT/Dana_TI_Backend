using System;

namespace TennisClub.Common.MemberRole
{
    public class MemberRoleReadDTO
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public byte RoleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
