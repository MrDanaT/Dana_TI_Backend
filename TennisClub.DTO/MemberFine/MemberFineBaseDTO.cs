using System;

namespace TennisClub.DTO.MemberFine
{
    public class MemberFineBaseDTO
    {
        public int FineNumber { get; set; }
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public DateTime HandoutDate { get; set; }
    }
}
