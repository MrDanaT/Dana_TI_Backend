using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.MemberRepository;

namespace TennisClub.BL
{
    public class MemberLogic
    {
        private readonly IMemberRepository _repo;

        public MemberLogic(IMemberRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Member> GetAllMembers()
        {
            IEnumerable<Member> memberItems = _repo.GetAll();

            return memberItems;
        }

        public Member GetMemberById(int id)
        {
            Member memberFromRepo = _repo.GetById(id);

            return memberFromRepo;
        }

        public void CreateMember(Member member)
        {
            _repo.Create(member);
            _repo.SaveChanges();
        }

        public void PartialMemberUpdate(Member member)
        {
            _repo.Update(member);
            _repo.SaveChanges();
        }

        public void DeleteMember(Member member)
        {
            _repo.Delete(member);
            _repo.SaveChanges();
        }

        public IEnumerable<Member> GetAllActiveMembers()
        {
            IEnumerable<Member> memberItems = _repo.GetAllActiveMembers();

            return memberItems;
        }
    }
}
