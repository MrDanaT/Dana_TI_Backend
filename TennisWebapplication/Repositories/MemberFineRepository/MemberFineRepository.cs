using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.MemberFineRepository
{
    public class MemberFineRepository : IMemberFineRepository
    {
        private readonly TennisClubContext _context;

        public MemberFineRepository(TennisClubContext context)
        {
            _context = context;
        }

        public void CreateMemberFine(MemberFine memberFine)
        {
            if (memberFine == null)
            {
                throw new ArgumentNullException(nameof(memberFine));
            }

            _context.MemberFines.Add(memberFine);
        }

        public IEnumerable<MemberFine> GetAllMemberFines()
        {
            return _context.MemberFines.AsNoTracking().ToList();
        }

        public IEnumerable<MemberFine> GetMemberFinesByMember(Member member)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateMemberFine(MemberFine memberFine)
        {
            if (memberFine.PaymentDate != null)
            {
                // TODO: welk is beter?
                // throw new ArgumentException(nameof(memberFine));
                _context.Entry(memberFine).State = EntityState.Unchanged;
            }
        }
    }
}
