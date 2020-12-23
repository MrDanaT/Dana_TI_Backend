using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<MemberReadDTO> GetAllMembers(string federationNr, string firstName, string lastName,
            string location)
        {
            var memberItems = _unitOfWork.Members.GetAll();

            memberItems = GetFilteredMemberItems(memberItems, federationNr, firstName, lastName, location);

            return memberItems;
        }

        public MemberReadDTO GetMemberById(int id)
        {
            var memberFromRepo = _unitOfWork.Members.GetById(id);

            return memberFromRepo;
        }

        public MemberReadDTO CreateMember(MemberCreateDTO member)
        {
            var createdMember = _unitOfWork.Members.Create(member);
            _unitOfWork.Commit();
            return createdMember;
        }

        public void DeleteMember(int id)
        {
            _unitOfWork.Members.Delete(id);
            _unitOfWork.Commit();
        }

        public IEnumerable<MemberReadDTO> GetAllActiveMembers(string federationNr, string firstName, string lastName,
            string location)
        {
            var memberItems = _unitOfWork.Members.GetAllActiveMembers();

            memberItems = GetFilteredMemberItems(memberItems, federationNr, firstName, lastName, location);

            return memberItems;
        }

        public void UpdateMember(int id, MemberUpdateDTO memberToPatch)
        {
            _unitOfWork.Members.Update(id, memberToPatch);
            _unitOfWork.Commit();
        }

        private IEnumerable<MemberReadDTO> GetFilteredMemberItems(IEnumerable<MemberReadDTO> memberItems,
            string federationNr, string firstName, string lastName, string location)
        {
            if (!string.IsNullOrEmpty(federationNr))
                memberItems = memberItems.Where(x => x.FederationNr.ToLower().Contains(federationNr.ToLower()));
            if (!string.IsNullOrEmpty(firstName))
                memberItems = memberItems.Where(x => x.FirstName.ToLower().Contains(firstName.ToLower()));
            if (!string.IsNullOrEmpty(lastName))
                memberItems = memberItems.Where(x => x.LastName.ToLower().Contains(lastName.ToLower()));
            if (!string.IsNullOrEmpty(location))
            {
                location = location.ToLower();
                var tmp = memberItems.Where(x => x.City.ToLower().Contains(location));
                if (tmp.Count() > 0) memberItems = tmp;
                tmp = memberItems.Where(x => x.Zipcode.ToLower().Contains(location));
                if (tmp.Count() > 0) memberItems = tmp;
            }

            return memberItems;
        }

        public IEnumerable<MemberReadDTO> GetAllActiveSpelerMembers()
        {
            var memberItems = _unitOfWork.Members.GetAllActiveSpelerMembers();
            return memberItems;
        }
    }
}