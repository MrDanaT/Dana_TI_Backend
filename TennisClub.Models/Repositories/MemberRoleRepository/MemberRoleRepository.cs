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

        public void CreateMemberRole(MemberRole memberRole)
        {
            if (memberRole == null)
            {
                throw new ArgumentNullException(nameof(memberRole));
            }

            _context.MemberRoles.Add(memberRole);
        }

        public IEnumerable<Member> GetMembersByRoles(IEnumerable<Role> roles)
        {
            IQueryable<Member> filteredMembersByRoles = _context.MemberRoles
                .Where(mr => roles.Any(r => r.Id == mr.RoleId))
                .Select(mr => mr.MemberNavigation);

            return filteredMembersByRoles;
        }

        public IEnumerable<Role> GetRolesByMember(Member member)
        {
            IQueryable<Role> filteredMemberRoles = _context.MemberRoles
                .Where(mr => mr.MemberId == member.Id)
                .Select(mr => mr.RoleNavigation);

            return filteredMemberRoles;
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateMemberRole(MemberRole memberRole)
        {
            // Nothing
        }


    }
}
