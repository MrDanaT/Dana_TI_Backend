using System;

namespace TennisClub.DTO.MemberFine
{
    public class MemberFineCreateDTO : MemberFineBaseDTO
    {
        public DateTime? PaymentDate { get; set; }
    }
}
