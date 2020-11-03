using System.Collections.Generic;
using TennisClub.Common.Member;
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

        public IEnumerable<MemberReadDTO> GetAllMembers()
        {
            IEnumerable<MemberReadDTO> memberItems = _unitOfWork.Members.GetAll();

            return memberItems;
        }

        public MemberReadDTO GetMemberById(int id)
        {
            MemberReadDTO memberFromRepo = _unitOfWork.Members.GetById(id);

            return memberFromRepo;
        }

        public MemberReadDTO CreateMember(MemberCreateDTO member)
        {
            MemberReadDTO createdMember = _unitOfWork.Members.Create(member);
            _unitOfWork.Commit();
            return createdMember;
        }

        public void DeleteMember(int id)
        {
            _unitOfWork.Members.Delete(id);
            _unitOfWork.Commit();
        }

        public IEnumerable<MemberReadDTO> GetAllActiveMembers()
        {
            IEnumerable<MemberReadDTO> memberItems = _unitOfWork.Members.GetAllActiveMembers();

            return memberItems;
        }

        public void UpdateMember(int id, MemberUpdateDTO memberToPatch)
        {
            _unitOfWork.Members.Update(id, memberToPatch);
            _unitOfWork.Commit();
        }
    }
}
