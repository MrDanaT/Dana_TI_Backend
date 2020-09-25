using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisWebapplication.Models
{
    public class MemberRole
    {
        public int Id { get; set; }
        public virtual Member MemberId { get; set; }
        public virtual Role RoleId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}