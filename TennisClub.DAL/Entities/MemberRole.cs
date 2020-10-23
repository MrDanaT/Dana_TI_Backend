using System;

namespace TennisClub.DAL.Entities
{
    public class MemberRole
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int RoleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Member MemberNavigation { get; set; }
        public Role RoleNavigation { get; set; }
    }
}