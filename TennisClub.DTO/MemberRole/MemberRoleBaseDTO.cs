using System;

namespace TennisClub.DTO.MemberRole
{
    public class MemberRoleBaseDTO
    {
        public int MemberId { get; set; }
        public byte RoleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}