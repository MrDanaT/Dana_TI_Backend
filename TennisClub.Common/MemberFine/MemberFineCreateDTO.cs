using System;

namespace TennisClub.Common.MemberFine
{
    public class MemberFineCreateDTO
    {
        public int FineNumber { get; set; }
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public DateTime HandoutDate { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
