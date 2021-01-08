using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.Common;
using TennisClub.Common.Member;
using TennisClub.Common.MemberRole;
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
            if (roleIds.IsNull()) throw new ArgumentNullException();

            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            var itemsFromDB = TennisClubContext.MemberRoles
                .AsNoTracking()
                .Include(x => x.MemberNavigation)
                .Include(x => x.RoleNavigation)
                .Where(mr => roleIds.Any(r => r == mr.RoleId));

            return _mapper.Map<IEnumerable<MemberRoleReadDTO>>(itemsFromDB);
        }

        public IEnumerable<MemberRoleReadDTO> GetMemberRolesByMember(MemberReadDTO member)
        {
            if (member.IsNull()) throw new ArgumentNullException();

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

        public override MemberRoleReadDTO Create(MemberRoleCreateDTO entity)
        {
            if (entity.IsNull()) throw new ArgumentNullException(nameof(entity));

            var mappedObject = _mapper.Map<MemberRole>(entity);
            mappedObject.MemberNavigation = TennisClubContext.Members.Find(mappedObject.MemberId);
            mappedObject.RoleNavigation = TennisClubContext.Roles.Find(mappedObject.RoleId);
            TennisClubContext.MemberRoles.Add(mappedObject);
            TennisClubContext.SaveChanges();

            return _mapper.Map<MemberRoleReadDTO>(mappedObject);
        }

        public override MemberRoleReadDTO GetById(int id)
        {
            if (!id.IsValidId()) throw new NullReferenceException("Id is out of range");

            var itemFromDB = TennisClubContext.MemberRoles.Find(id);

            if (itemFromDB.IsNull()) throw new NullReferenceException("Object not found");

            itemFromDB.MemberNavigation = TennisClubContext.Members.Find(itemFromDB.MemberId);
            itemFromDB.RoleNavigation = TennisClubContext.Roles.Find(itemFromDB.RoleId);

            return _mapper.Map<MemberRoleReadDTO>(itemFromDB);
        }
    }
}