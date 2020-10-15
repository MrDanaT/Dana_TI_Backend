using Microsoft.AspNetCore.JsonPatch;
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

        public void DeleteMember(MemberReadDTO member)
        {
            _unitOfWork.Members.Delete(member);
            _unitOfWork.Commit();
        }

        public IEnumerable<MemberReadDTO> GetAllActiveMembers()
        {
            IEnumerable<MemberReadDTO> memberItems = _unitOfWork.Members.GetAllActiveMembers();

            return memberItems;
        }

        public MemberUpdateDTO GetUpdateDTOByReadDTO(MemberReadDTO entity)
        {
            return _unitOfWork.Members.GetUpdateDTOByReadDTO(entity);
        }

        public void UpdateMember(MemberUpdateDTO memberToPatch, MemberReadDTO memberModelFromRepo)
        {
            _unitOfWork.Members.MapUpdateDTOToReadDTO(memberToPatch, memberModelFromRepo);
            _unitOfWork.Commit();
        }
    }
}
