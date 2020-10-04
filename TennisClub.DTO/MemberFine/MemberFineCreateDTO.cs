using System;
using System.Collections.Generic;
using System.Text;

namespace TennisClub.DTO.MemberFine
{
    public class MemberFineCreateDTO : MemberFineBaseDTO
    {
        public DateTime? PaymentDate { get; set; }
    }
}
