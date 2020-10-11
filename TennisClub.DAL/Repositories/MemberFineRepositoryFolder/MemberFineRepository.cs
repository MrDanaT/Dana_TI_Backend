using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberFineRepositoryFolder
{
    public class MemberFineRepository : Repository<MemberFine>, IMemberFineRepository
    {
        public MemberFineRepository(TennisClubContext context)
           : base(context)
        { }

        public IEnumerable<MemberFine> GetMemberFinesByMember(Member member)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<MemberFine> memberFineItems = TennisClubContext.MemberFines
                .AsNoTracking()
                .Where(mf => mf.MemberId == member.Id)
                .Select(mf => mf);

            return memberFineItems.AsEnumerable();
        }

        private TennisClubContext TennisClubContext => Context;
    }
}
