using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRepositoryFolder
{
    public class MemberRepository : Repository<Member, MemberCreateDTO, MemberReadDTO, MemberUpdateDTO>, IMemberRepository
    {
        public MemberRepository(TennisClubContext context, IMapper mapper)
           : base(context, mapper)
        { }

        public IEnumerable<MemberReadDTO> GetAllActiveMembers()
        {
            List<Member> itemsFromDB = TennisClubContext.Members.AsNoTracking().Where(m => m.Deleted == false).ToList();
            return _mapper.Map<IEnumerable<MemberReadDTO>>(itemsFromDB);
        }

        private TennisClubContext TennisClubContext => Context;
    }
}
