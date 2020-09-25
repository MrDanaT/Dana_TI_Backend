using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisWebapplication.Models
{
    public class MemberFine
    {
        public int Id { get; set; }
        public int FineNumber { get; set; }
        public virtual Member MemberId { get; set; }
        public decimal Amount { get; set; }
        public DateTime HandoutDate { get; set; }
        public DateTime? PaymentDate { get; set; }

        // public virtual Game GameId { get; set; }

    }
}