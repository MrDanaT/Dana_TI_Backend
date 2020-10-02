using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        //  public virtual Game GameId { get; set; } // TODO: vragen.

    }
}