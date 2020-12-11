using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
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
            List<Member> itemsFromDB = TennisClubContext.Members.AsNoTracking()
                .Where(m => m.Deleted == false)
                .Include(x => x.GenderNavigation)
                .ToList();

            return _mapper.Map<IEnumerable<MemberReadDTO>>(itemsFromDB);
        }

        public override void Delete(int id)
        {
            TennisClubContext.Database.ExecuteSqlRaw($"dbo.SoftDeleteMember @pId={id}");
        }

        public override IEnumerable<MemberReadDTO> GetAll()
        {
            List<Member> itemsFromDB = TennisClubContext.Members.AsNoTracking()
               .Include(x => x.GenderNavigation)
               .ToList();

            return _mapper.Map<IEnumerable<MemberReadDTO>>(itemsFromDB);
        }

        public override MemberReadDTO GetById(int id)
        {
            if (id < 0)
            {
                throw new NullReferenceException("Id is out of range");
            }

            Member itemFromDB = Context.Members.Find(id);

            if (itemFromDB == null)
            {
                throw new NullReferenceException("Object not found");
            }

            return _mapper.Map<MemberReadDTO>(itemFromDB);
        }

        private TennisClubContext TennisClubContext => Context;
    }
}
