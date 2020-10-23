using System.Collections.Generic;
using TennisClub.Common.Member;
using TennisClub.Common.MemberFine;
using TennisClub.DAL.Repositories;

namespace TennisClub.BL.MemberFineServiceFolder
{
    public class MemberFineService : IMemberFineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberFineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MemberFineReadDTO> GetAllMemberFines()
        {
            IEnumerable<MemberFineReadDTO> memberFineItems = _unitOfWork.MemberFines.GetAll();

            return memberFineItems;
        }

        public MemberFineReadDTO GetMemberFineById(int id)
        {
            MemberFineReadDTO memberFine = _unitOfWork.MemberFines.GetById(id);

            return memberFine;
        }

        public MemberFineReadDTO CreateMemberFine(MemberFineCreateDTO memberFine)
        {
            MemberFineReadDTO createdMemberFine = _unitOfWork.MemberFines.Create(memberFine);
            _unitOfWork.Commit();
            return createdMemberFine;
        }

        public IEnumerable<MemberFineReadDTO> GetMemberFinesByMemberId(int id)
        {
            MemberReadDTO memberFromRepo = _unitOfWork.Members.GetById(id);
            IEnumerable<MemberFineReadDTO> memberFineItems = _unitOfWork.MemberFines.GetMemberFinesByMember(memberFromRepo);

            return memberFineItems;
        }

        public MemberFineUpdateDTO GetUpdateDTOByReadDTO(MemberFineReadDTO entity)
        {
            return _unitOfWork.MemberFines.GetUpdateDTOByReadDTO(entity);
        }

        public void UpdateMemberFine(MemberFineUpdateDTO modelFineToPatch, MemberFineReadDTO memberFineModelFromRepo)
        {
            _unitOfWork.MemberFines.MapUpdateDTOToReadDTO(modelFineToPatch, memberFineModelFromRepo);
            _unitOfWork.Commit();
        }
    }
}
