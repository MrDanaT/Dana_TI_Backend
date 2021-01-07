using System.Collections.Generic;
using System.Linq;
using TennisClub.Common.MemberRole;
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
            IEnumerable<MemberRoleReadDTO>? memberRoleItems = _unitOfWork.MemberRoles.GetAll();

            return memberRoleItems;
        }

        public MemberRoleReadDTO GetMemberRoleById(int id)
        {
            MemberRoleReadDTO? memberRoleItem = _unitOfWork.MemberRoles.GetById(id);

            return memberRoleItem;
        }

        public MemberRoleReadDTO CreateMemberRole(MemberRoleCreateDTO memberRole)
        {
            MemberRoleReadDTO? createdMemberRole = _unitOfWork.MemberRoles.Create(memberRole);
            _unitOfWork.Commit();
            return createdMemberRole;
        }

        public IEnumerable<MemberRoleReadDTO> GetMemberRolesByMemberId(int id)
        {
            // TODO: Dit nakijken samen met repository.
            Common.Member.MemberReadDTO? memberFromRepo = _unitOfWork.Members.GetById(id);
            IEnumerable<MemberRoleReadDTO>? roleItems = _unitOfWork.MemberRoles.GetMemberRolesByMember(memberFromRepo);

            return roleItems;
        }

        public IEnumerable<MemberRoleReadDTO> GetMemberRolesByRoleIds(string roleIds)
        {
            int[]? intIds = roleIds.Split(',').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToArray();
            IEnumerable<MemberRoleReadDTO>? memberItems = _unitOfWork.MemberRoles.GetMemberRolesByRoleIds(intIds);

            return memberItems;
        }

        public void UpdateMemberRole(int id, MemberRoleUpdateDTO updateDTO)
        {
            _unitOfWork.MemberRoles.Update(id, updateDTO);
            _unitOfWork.Commit();
        }
    }
}