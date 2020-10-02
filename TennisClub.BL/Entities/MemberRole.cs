using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisClub.BL.Entities
{
    public class MemberRole : BaseEntity
    {
        public int MemberId { get; set; }
        public byte RoleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual Member MemberNavigation { get; set; }
        public virtual Role RoleNavigation { get; set; }
    }
}