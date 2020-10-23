using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.Common.Member;
using TennisClub.Common.MemberFine;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberFineRepositoryFolder
{
    public class MemberFineRepository : Repository<MemberFine, MemberFineCreateDTO, MemberFineReadDTO, MemberFineUpdateDTO>, IMemberFineRepository
    {
        public MemberFineRepository(TennisClubContext context, IMapper mapper)
           : base(context, mapper)
        { }

        public IEnumerable<MemberFineReadDTO> GetMemberFinesByMember(MemberReadDTO member)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<MemberFine> memberFineItems = TennisClubContext.MemberFines
                .AsNoTracking()
                .Where(mf => mf.MemberId == member.Id)
                .Select(mf => mf);

            return _mapper.Map<IEnumerable<MemberFineReadDTO>>(memberFineItems);
        }

        private TennisClubContext TennisClubContext => Context;
    }
}
