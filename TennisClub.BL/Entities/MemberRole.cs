using System;

namespace TennisClub.BL.Entities
{
    public class MemberRole
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public byte RoleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual Member MemberNavigation { get; set; }
        public virtual Role RoleNavigation { get; set; }
    }
}