using System;
using System.Collections.Generic;
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

        public IEnumerable<Member> GetMembersByRoles(params Role[] roles)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRolesByMember(Member member)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateMemberRole(MemberRole memberRole)
        {
            throw new NotImplementedException();
        }
    }
}
