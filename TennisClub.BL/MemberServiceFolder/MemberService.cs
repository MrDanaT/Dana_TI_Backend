using System.Collections.Generic;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories;

namespace TennisClub.BL.MemberServiceFolder
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Member> GetAllMembers()
        {
            IEnumerable<Member> memberItems = _unitOfWork.Members.GetAll();

            return memberItems;
        }

        public Member GetMemberById(int id)
        {
            Member memberFromRepo = _unitOfWork.Members.GetById(id);

            return memberFromRepo;
        }

        public void CreateMember(Member member)
        {
            _unitOfWork.Members.Create(member);
            _unitOfWork.Commit();
        }

        public void UpdateMember(Member member)
        {
            _unitOfWork.Commit();
        }

        public void DeleteMember(Member member)
        {
            _unitOfWork.Members.Delete(member);
            _unitOfWork.Commit();
        }

        public IEnumerable<Member> GetAllActiveMembers()
        {
            IEnumerable<Member> memberItems = _unitOfWork.Members.GetAllActiveMembers();

            return memberItems;
        }
    }
}
