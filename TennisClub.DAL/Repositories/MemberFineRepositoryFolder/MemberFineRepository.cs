using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TennisClub.Common.Member;
using TennisClub.Common.MemberFine;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberFineRepositoryFolder
{
    public class MemberFineRepository :
        Repository<MemberFine, MemberFineCreateDTO, MemberFineReadDTO, MemberFineUpdateDTO>, IMemberFineRepository
    {
        public MemberFineRepository(TennisClubContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        private TennisClubContext TennisClubContext => Context;

        public override IEnumerable<MemberFineReadDTO> GetAll()
        {
            var memberFineItems = TennisClubContext.MemberFines
                .AsNoTracking()
                .Include(x => x.MemberNavigation);

            return _mapper.Map<IEnumerable<MemberFineReadDTO>>(memberFineItems);
        }

        public override MemberFineReadDTO Create(MemberFineCreateDTO entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            var mappedObject = _mapper.Map<MemberFine>(entity);
            mappedObject.MemberNavigation = TennisClubContext.Members.Find(mappedObject.MemberId);
            TennisClubContext.MemberFines.Add(mappedObject);
            TennisClubContext.SaveChanges();

            return _mapper.Map<MemberFineReadDTO>(mappedObject);
        }

        public override MemberFineReadDTO GetById(int id)
        {
            if (id < 0) throw new NullReferenceException("Id is out of range");

            var itemFromDB = Context.MemberFines.Find(id);
            if (itemFromDB == null) throw new NullReferenceException("Object not found");

            itemFromDB.MemberNavigation = TennisClubContext.Members.Find(itemFromDB.MemberId);

            return _mapper.Map<MemberFineReadDTO>(itemFromDB);
        }

        public IEnumerable<MemberFineReadDTO> GetMemberFinesByMember(MemberReadDTO member)
        {
            var memberFineItems = TennisClubContext.MemberFines
                .AsNoTracking()
                .Include(x => x.MemberNavigation)
                .Where(mf => mf.MemberId == member.Id)
                .Select(mf => mf);

            return _mapper.Map<IEnumerable<MemberFineReadDTO>>(memberFineItems);
        }

        public override void Update(int id, MemberFineUpdateDTO entity)
        {
            if (id < 0) throw new NullReferenceException("Id is out of range");

            var memberFineFromRepo = Context.MemberFines.Find(id);

            if (memberFineFromRepo == null) throw new NullReferenceException("Object not found");

            if (memberFineFromRepo.PaymentDate == null) base.Update(id, entity);
        }

        public override void Delete(int id)
        {
            // Do nothing
        }
    }
}