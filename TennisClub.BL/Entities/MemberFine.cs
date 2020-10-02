using System;
using TennisClub.BL.Entities.Common;

namespace TennisClub.BL.Entities
{
    public class MemberFine : BaseEntity
    {
        public int FineNumber { get; set; }
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public DateTime HandoutDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public virtual Member MemberNavigation { get; set; }
    }
}