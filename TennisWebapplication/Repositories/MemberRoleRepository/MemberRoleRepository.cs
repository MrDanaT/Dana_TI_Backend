using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.MemberRoleRepository
{
    public class MemberRoleRepository : IMemberRoleRepository
    {
        public void CreateMemberRole(MemberRole role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MemberRole> GetMemberRolesByMember(Member member)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetMembersByRole(params Role[] role)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateMemberRole(MemberRole role)
        {
            throw new NotImplementedException();
        }
    }
}
