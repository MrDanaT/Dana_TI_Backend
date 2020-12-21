using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRepositoryFolder
{
    public class MemberRepository : Repository<Member, MemberCreateDTO, MemberReadDTO, MemberUpdateDTO>,
        IMemberRepository
    {
        public MemberRepository(TennisClubContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        private TennisClubContext TennisClubContext => Context;


        public IEnumerable<MemberReadDTO> GetAllActiveMembers()
        {
            var itemsFromDB = TennisClubContext.Members.AsNoTracking()
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
            var itemsFromDB = TennisClubContext.Members.AsNoTracking()
                .Include(x => x.GenderNavigation)
                .ToList();

            return _mapper.Map<IEnumerable<MemberReadDTO>>(itemsFromDB);
        }

        public override MemberReadDTO GetById(int id)
        {
            if (id < 0) throw new NullReferenceException("Id is out of range");

            var itemFromDB = Context.Members.Find(id);

            if (itemFromDB == null) throw new NullReferenceException("Object not found");

            return _mapper.Map<MemberReadDTO>(itemFromDB);
        }

        public override MemberReadDTO Create(MemberCreateDTO entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var mappedObject = _mapper.Map<Member>(entity);
            mappedObject.Deleted = false;
            mappedObject.GenderNavigation = TennisClubContext.Genders.Find(mappedObject.GenderId);
            TennisClubContext.Members.Add(mappedObject);
            TennisClubContext.SaveChanges();

            return _mapper.Map<MemberReadDTO>(mappedObject);
        }
    }
}