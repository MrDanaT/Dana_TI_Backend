using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRoleRepositoryFolder
{
    public class MemberRoleRepository : Repository<MemberRole>, IMemberRoleRepository
    {
        public MemberRoleRepository(TennisClubContext context)
           : base(context)
        { }


        public IEnumerable<Member> GetMembersByRoles(List<Role> roles)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<Member> members = TennisClubContext.MemberRoles
                .AsNoTracking()
                .Where(mr => roles.Any(r => r == mr.RoleNavigation))
                .Select(mr => mr.MemberNavigation);

            return members.AsEnumerable();
        }

        public IEnumerable<Role> GetRolesByMember(Member member)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<Role> roles = TennisClubContext.MemberRoles
                .AsNoTracking()
                .Where(mr => mr.MemberId == member.Id)
                .Select(mr => mr.RoleNavigation);

            return roles.AsEnumerable(); ;
        }


        private TennisClubContext TennisClubContext => Context;
    }
}
