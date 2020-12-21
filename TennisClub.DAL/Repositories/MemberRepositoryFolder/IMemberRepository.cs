using System.Collections.Generic;
using TennisClub.Common.Member;

namespace TennisClub.DAL.Repositories.MemberRepositoryFolder
{
    public interface IMemberRepository : IRepository<MemberCreateDTO, MemberReadDTO, MemberUpdateDTO>
    {
        IEnumerable<MemberReadDTO> GetAllActiveMembers();
    }
}