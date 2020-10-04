using System;

namespace TennisClub.DTO.MemberRole
{
    public class MemberRoleReadDTO : MemberRoleBaseDTO
    {
        public int Id { get; set; }
        public DateTime EndDate { get; set; }
    }
}
