using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberFineRepository
{
    public class MemberFineRepository : IMemberFineRepository
    {
        private readonly TennisClubContext _context;

        public MemberFineRepository(TennisClubContext context)
        {
            _context = context;
        }

        public void Create(MemberFine memberFine)
        {
            if (memberFine == null)
            {
                throw new ArgumentNullException(nameof(memberFine));
            }

            _context.MemberFines.Add(memberFine);
        }

        public void Delete(MemberFine entity)
        {
            // Nothing
        }

        public IEnumerable<MemberFine> GetAll()
        {
            return _context.MemberFines.AsNoTracking().ToList();
        }

        public MemberFine GetById(int id)
        {
            return _context.MemberFines.FirstOrDefault(mf => mf.Id == id);
        }

        public IEnumerable<MemberFine> GetMemberFinesByMember(Member member)
        {
            // TODO: zie of het ("=.AsNoTracking()) sneller of trager gaat hierdoor.
            IQueryable<MemberFine> memberFineItems = _context.MemberFines
                .AsNoTracking()
                .Where(mf => mf.MemberId == member.Id)
                .Select(mf => mf);

            return memberFineItems.AsEnumerable();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(MemberFine memberFine)
        {
            // Nothing
        }
    }
}
