using System.Collections.Generic;
using TennisClub.Common.Member;
using TennisClub.Common.MemberFine;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberFineRepositoryFolder
{
    public interface IMemberFineRepository : IRepository< MemberFineCreateDTO, MemberFineReadDTO, MemberFineUpdateDTO>
    {
        IEnumerable<MemberFineReadDTO> GetMemberFinesByMember(MemberReadDTO member);
    }
}
