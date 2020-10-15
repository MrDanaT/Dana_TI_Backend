using System.Collections.Generic;
using TennisClub.Common.Member;
using TennisClub.Common.MemberRole;
using TennisClub.Common.Role;
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

        public IEnumerable<MemberRoleReadDTO> GetAllMemberRoles()
        {
            IEnumerable<MemberRoleReadDTO> memberRoleItems = _unitOfWork.MemberRoles.GetAll();

            return memberRoleItems;
        }

        public MemberRoleReadDTO GetMemberRoleById(int id)
        {
            MemberRoleReadDTO memberRoleItem = _unitOfWork.MemberRoles.GetById(id);

            return memberRoleItem;
        }

        public MemberRoleReadDTO CreateMemberRole(MemberRoleCreateDTO memberRole)
        {
            var createdMemberRole = _unitOfWork.MemberRoles.Create(memberRole);
            _unitOfWork.Commit();
            return createdMemberRole;
        }

        public IEnumerable<RoleReadDTO> GetRolesByMemberId(int id)
        {
            // TODO: Dit nakijken samen met repository.
            MemberReadDTO memberFromRepo = _unitOfWork.Members.GetById(id);
            IEnumerable<RoleReadDTO> roleItems = _unitOfWork.MemberRoles.GetRolesByMember(memberFromRepo);

            return roleItems;
        }

        public IEnumerable<MemberReadDTO> GetMembersByRoles(List<RoleReadDTO> roles)
        {
            // TODO: Dit nakijken samen met repository.
            IEnumerable<MemberReadDTO> memberItems = _unitOfWork.MemberRoles.GetMembersByRoles(roles);

            return memberItems;
        }

        public MemberRoleUpdateDTO GetUpdateDTOByReadDTO(MemberRoleReadDTO entity)
        {
            return _unitOfWork.MemberRoles.GetUpdateDTOByReadDTO(entity);
        }

        public void UpdateMemberRole(MemberRoleUpdateDTO memberRoleToPatch, MemberRoleReadDTO memberRoleModelFromRepo)
        {
            _unitOfWork.MemberRoles.MapUpdateDTOToReadDTO(memberRoleToPatch, memberRoleModelFromRepo);
            _unitOfWork.Commit();
        }
    }
}
