using System.Collections.Generic;
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
            var memberFineItems = _unitOfWork.MemberFines.GetAll();
            return memberFineItems;
        }

        public MemberFineReadDTO GetMemberFineById(int id)
        {
            var memberFine = _unitOfWork.MemberFines.GetById(id);
            return memberFine;
        }

        public MemberFineReadDTO CreateMemberFine(MemberFineCreateDTO memberFine)
        {
            var createdMemberFine = _unitOfWork.MemberFines.Create(memberFine);
            _unitOfWork.Commit();
            return createdMemberFine;
        }

        public IEnumerable<MemberFineReadDTO> GetMemberFinesByMemberId(int id)
        {
            var memberFromRepo = _unitOfWork.Members.GetById(id);
            var memberFineItems = _unitOfWork.MemberFines.GetMemberFinesByMember(memberFromRepo);

            return memberFineItems;
        }

        public void UpdateMemberFine(int id, MemberFineUpdateDTO updateDTO)
        {
            _unitOfWork.MemberFines.Update(id, updateDTO);
            _unitOfWork.Commit();
        }
    }
}