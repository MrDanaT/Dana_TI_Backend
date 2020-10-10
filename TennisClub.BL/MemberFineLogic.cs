using System;
using System.Collections.Generic;
using System.Text;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.MemberFineRepository;
using TennisClub.DAL.Repositories.MemberRepository;

namespace TennisClub.BL
{
    public class MemberFineLogic
    {
        private readonly IMemberFineRepository _repo;
        private readonly IMemberRepository _memberRepo;

        public MemberFineLogic(IMemberFineRepository repo, IMemberRepository memberRepo)
        {
            _repo = repo;
            _memberRepo = memberRepo;
        }

        public IEnumerable<MemberFine> GetAllMemberFines()
        {
            IEnumerable<MemberFine> memberFineItems = _repo.GetAll();

            return memberFineItems;
        }

        public MemberFine GetMemberFineById(int id)
        {
            MemberFine memberFine = _repo.GetById(id);

            return memberFine;
        }

        public void CreateMemberFine(MemberFine memberFine)
        {
            _repo.Create(memberFine);
            _repo.SaveChanges();
        }

        public void PartialMemberFineUpdate(MemberFine memberFine)
        {
            _repo.Update(memberFine);
            _repo.SaveChanges();
        }

        public IEnumerable<MemberFine> GetMemberFinesByMemberId(int id)
        {
            Member memberFromRepo = _memberRepo.GetById(id);
            IEnumerable<MemberFine> memberFineItems = _repo.GetMemberFinesByMember(memberFromRepo);

            return memberFineItems;
        }
    }
}
