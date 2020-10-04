using System;

namespace TennisClub.DTO.MemberFine
{
    public class MemberFineReadDTO : MemberFineBaseDTO
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
