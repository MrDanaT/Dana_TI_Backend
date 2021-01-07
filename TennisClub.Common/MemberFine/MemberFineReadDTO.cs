using System;

namespace TennisClub.Common.MemberFine
{
    public class MemberFineReadDTO : BaseReadDTO
    {
        public int FineNumber { get; set; }
        public int MemberId { get; set; }
        public string MemberFullName { get; set; }
        public decimal Amount { get; set; }
        public DateTime HandoutDate { get; set; }
        public DateTime PaymentDate { get; set; }

        public override string ToString()
        {
            return FineNumber.ToString();
        }
    }
}