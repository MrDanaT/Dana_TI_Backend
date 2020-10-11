using System.Collections.Generic;
using TennisClub.DAL.Entities;
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

        public IEnumerable<MemberFine> GetAllMemberFines()
        {
            IEnumerable<MemberFine> memberFineItems = _unitOfWork.MemberFines.GetAll();

            return memberFineItems;
        }

        public MemberFine GetMemberFineById(int id)
        {
            MemberFine memberFine = _unitOfWork.MemberFines.GetById(id);

            return memberFine;
        }

        public void CreateMemberFine(MemberFine memberFine)
        {
            _unitOfWork.MemberFines.Create(memberFine);
            _unitOfWork.Commit();
        }

        public void UpdateMemberFine(MemberFine memberFine)
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<MemberFine> GetMemberFinesByMemberId(int id)
        {
            Member memberFromRepo = _unitOfWork.Members.GetById(id);
            IEnumerable<MemberFine> memberFineItems = _unitOfWork.MemberFines.GetMemberFinesByMember(memberFromRepo);

            return memberFineItems;
        }

    }
}
