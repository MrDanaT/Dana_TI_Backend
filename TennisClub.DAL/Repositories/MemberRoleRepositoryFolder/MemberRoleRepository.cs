using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
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



        public IEnumerable<MemberRoleReadDTO> GetMemberRolesByRoles(List<RoleReadDTO> roles)
        {
            if (roles == null)
            {
                throw new ArgumentNullException();
            }

            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<MemberRole> itemsFromDB = TennisClubContext.MemberRoles
                    .AsNoTracking()
                    .Include(x => x.MemberNavigation)
                    .Include(x => x.RoleNavigation)
                    .Where(mr => roles.Any(r => r.Id == mr.MemberId));

            return _mapper.Map<IEnumerable<MemberRoleReadDTO>>(itemsFromDB);
        }

        public IEnumerable<MemberRoleReadDTO> GetMemberRolesByMember(MemberReadDTO member)
        {
            if (member == null)
            {
                throw new ArgumentNullException();
            }

            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<MemberRole> itemsFromDB = TennisClubContext.MemberRoles
                    .AsNoTracking()
                    .Include(x => x.MemberNavigation)
                    .Include(x => x.RoleNavigation)
                    .Where(mr => mr.MemberId == member.Id);

            return _mapper.Map<IEnumerable<MemberRoleReadDTO>>(itemsFromDB);
        }

        public override void Delete(int id)
        {
            // Do nothing
        }

        public override IEnumerable<MemberRoleReadDTO> GetAll()
        {
            List<MemberRole> itemsFromDB = TennisClubContext.MemberRoles
                  .AsNoTracking()
                  .Include(x => x.MemberNavigation)
                  .Include(x => x.RoleNavigation)
                  .ToList();

            return _mapper.Map<IEnumerable<MemberRoleReadDTO>>(itemsFromDB);
        }

        private TennisClubContext TennisClubContext => Context;
    }
}
