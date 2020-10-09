using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Repositories.MemberRepository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly TennisClubContext _context;

        public MemberRepository(TennisClubContext context)
        {
            _context = context;
        }

        public void Create(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            _context.Members.Add(member);
        }

        public void Delete(Member member)
        {
            if (member == null)
            {
                throw new ArgumentNullException(nameof(member));
            }

            member.Deleted = true;
        }

        public IEnumerable<Member> GetAllActiveMembers()
        {
            return _context.Members.AsNoTracking().Where(m => m.Deleted == false).ToList();
        }

        public IEnumerable<Member> GetAll()
        {
            return _context.Members.AsNoTracking().ToList();
        }

        public Member GetById(int id)
        {
            return _context.Members.FirstOrDefault(m => m.Id == id);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(Member member)
        {
            //Nothing
        }
    }
}
