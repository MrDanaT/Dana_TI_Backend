using System.Collections.Generic;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories;

namespace TennisClub.BL.MemberRoleServiceFolder
{
    public class MemberRoleService : IMemberRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MemberRole> GetAllMemberRoles()
        {
            IEnumerable<MemberRole> memberRoleItems = _unitOfWork.MemberRoles.GetAll();

            return memberRoleItems;
        }

        public MemberRole GetMemberRoleById(int id)
        {
            MemberRole memberRoleItem = _unitOfWork.MemberRoles.GetById(id);

            return memberRoleItem;
        }

        public void CreateMemberRole(MemberRole memberRole)
        {
            _unitOfWork.MemberRoles.Create(memberRole);
            _unitOfWork.Commit();
        }

        public void UpdateMemberRole(MemberRole memberRole)
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<Role> GetRolesByMemberId(int id)
        {
            // TODO: Dit nakijken samen met repository.
            Member memberFromRepo = _unitOfWork.Members.GetById(id);
            IEnumerable<Role> roleItems = _unitOfWork.MemberRoles.GetRolesByMember(memberFromRepo);

            return roleItems;
        }

        public IEnumerable<Member> GetMembersByRoles(List<Role> roles)
        {
            // TODO: Dit nakijken samen met repository.
            IEnumerable<Member> memberItems = _unitOfWork.MemberRoles.GetMembersByRoles(roles);

            return memberItems;
        }
    }
}
