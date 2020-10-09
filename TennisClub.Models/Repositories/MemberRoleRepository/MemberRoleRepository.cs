using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.MemberRoleRepository
{
    public class MemberRoleRepository : IMemberRoleRepository
    {
        private readonly TennisClubContext _context;

        public MemberRoleRepository(TennisClubContext context)
        {
            _context = context;
        }

        public void Create(MemberRole memberRole)
        {
            if (memberRole == null)
            {
                throw new ArgumentNullException(nameof(memberRole));
            }

            _context.MemberRoles.Add(memberRole);
        }

        public IEnumerable<MemberRole> GetAllMemberRoles()
        {
            return _context.MemberRoles.AsNoTracking().ToList();
        }

        public MemberRole GetMemberRoleById(int id)
        {
            return _context.MemberRoles.FirstOrDefault(mr => mr.Id == id);
        }

        public IEnumerable<Member> GetMembersByRoles(List<string> roles)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<Member> members = _context.MemberRoles
                .AsNoTracking()
                .Where(mr => roles.Any(r => r == mr.RoleNavigation.Name))
                .Select(mr => mr.MemberNavigation);

            return members.AsEnumerable();
        }

        public IEnumerable<Role> GetRolesByMember(Member member)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<Role> roles = _context.MemberRoles
                .AsNoTracking()
                .Where(mr => mr.MemberId == member.Id)
                .Select(mr => mr.RoleNavigation);

            return roles.AsEnumerable(); ;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(MemberRole memberRole)
        {
            // Nothing
        }


    }
}
