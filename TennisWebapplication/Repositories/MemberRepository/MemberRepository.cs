using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.MemberRepository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly TennisClubContext _context;

        public MemberRepository(TennisClubContext context)
        {
            _context = context;
        }

        public void CreateMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            _context.Members.Add(member);
        }

        public void DeleteMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            _context.Members.Remove(member);
        }

        public IEnumerable<Member> GetAllMembers()
        {
            return _context.Members.AsNoTracking().ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateMember(Member member)
        {
            //Nothing
        }
    }
}
