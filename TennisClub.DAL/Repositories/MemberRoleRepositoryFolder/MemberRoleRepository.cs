using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.Common.Member;
using TennisClub.Common.MemberRole;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRoleRepositoryFolder
{
    public class MemberRoleRepository : Repository<MemberRole, MemberRoleCreateDTO, MemberRoleReadDTO, MemberRoleUpdateDTO>, IMemberRoleRepository
    {
        public MemberRoleRepository(TennisClubContext context, IMapper mapper)
           : base(context, mapper)
        { }


        public IEnumerable<MemberReadDTO> GetMembersByRoles(List<RoleReadDTO> roles)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<Member> members = TennisClubContext.MemberRoles
                .AsNoTracking()
                .Where(mr => roles.Any(r => r.Id == mr.MemberId))
                .Select(mr => mr.MemberNavigation);

            return _mapper.Map<IEnumerable<MemberReadDTO>>(members);
        }

        public IEnumerable<RoleReadDTO> GetRolesByMember(MemberReadDTO member)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<Role> roles = TennisClubContext.MemberRoles
                .AsNoTracking()
                .Where(mr => mr.MemberId == member.Id)
                .Select(mr => mr.RoleNavigation);

            return _mapper.Map<IEnumerable<RoleReadDTO>>(roles);
        }

        public override void Delete(int id)
        {
            // Do nothing
        }

        private TennisClubContext TennisClubContext => Context;
    }
}
