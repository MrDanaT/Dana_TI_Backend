using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Repositories.MemberRepository
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
            {
                throw new ArgumentNullException(nameof(member));
            }

            _context.Members.Add(member);
        }

        public void DeleteMember(Member member)
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

        public IEnumerable<Member> GetAllMembers()
        {
            return _context.Members.AsNoTracking().ToList();
        }

        public Member GetMemberById(int id)
        {
            return _context.Members.FirstOrDefault(m => m.Id == id);
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
