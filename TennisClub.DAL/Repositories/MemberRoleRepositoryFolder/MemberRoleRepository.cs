using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TennisClub.Common.Member;
using TennisClub.Common.MemberRole;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRoleRepositoryFolder
{
    public class MemberRoleRepository :
        Repository<MemberRole, MemberRoleCreateDTO, MemberRoleReadDTO, MemberRoleUpdateDTO>, IMemberRoleRepository
    {
        public MemberRoleRepository(TennisClubContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        private TennisClubContext TennisClubContext => Context;


        public IEnumerable<MemberRoleReadDTO> GetMemberRolesByRoleIds(int[] roleIds)
        {
            if (roleIds == null) throw new ArgumentNullException();

            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            var itemsFromDB = TennisClubContext.MemberRoles
                .AsNoTracking()
                .Include(x => x.MemberNavigation)
                .Include(x => x.RoleNavigation)
                .Where(mr => roleIds.Any(r => r == mr.MemberId));

            return _mapper.Map<IEnumerable<MemberRoleReadDTO>>(itemsFromDB);
        }

        public IEnumerable<MemberRoleReadDTO> GetMemberRolesByMember(MemberReadDTO member)
        {
            if (member == null) throw new ArgumentNullException();

            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            var itemsFromDB = TennisClubContext.MemberRoles
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
            var itemsFromDB = TennisClubContext.MemberRoles
                .AsNoTracking()
                .Include(x => x.MemberNavigation)
                .Include(x => x.RoleNavigation)
                .ToList();

            return _mapper.Map<IEnumerable<MemberRoleReadDTO>>(itemsFromDB);
        }
    }
}