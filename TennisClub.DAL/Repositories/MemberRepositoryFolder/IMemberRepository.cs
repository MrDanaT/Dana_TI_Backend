using System.Collections.Generic;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRepositoryFolder
{
    public interface IMemberRepository : IRepository< MemberCreateDTO, MemberReadDTO, MemberUpdateDTO, int>
    {
        IEnumerable<MemberReadDTO> GetAllActiveMembers();
    }
}
