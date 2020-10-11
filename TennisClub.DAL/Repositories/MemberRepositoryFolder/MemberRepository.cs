using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRepositoryFolder
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        public MemberRepository(TennisClubContext context)
           : base(context)
        { }

        public IEnumerable<Member> GetAllActiveMembers()
        {
            return TennisClubContext.Members.AsNoTracking().Where(m => m.Deleted == false).ToList();
        }

        private TennisClubContext TennisClubContext => Context;
    }
}
