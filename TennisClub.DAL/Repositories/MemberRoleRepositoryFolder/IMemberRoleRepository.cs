using System.Collections.Generic;
using TennisClub.Common.Member;
using TennisClub.Common.MemberRole;

namespace TennisClub.DAL.Repositories.MemberRoleRepositoryFolder
{
    public interface IMemberRoleRepository : IRepository<MemberRoleCreateDTO, MemberRoleReadDTO, MemberRoleUpdateDTO>
    {
        IEnumerable<MemberRoleReadDTO> GetMemberRolesByRoleIds(int[] roleIds);
        IEnumerable<MemberRoleReadDTO> GetMemberRolesByMember(MemberReadDTO member);
    }
}