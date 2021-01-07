using System.Collections.Generic;
using TennisClub.Common.Role;
using TennisClub.DAL.Repositories;

namespace TennisClub.BL.RoleServiceFolder
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<RoleReadDTO> GetAllRoles()
        {
            IEnumerable<RoleReadDTO>? roleItems = _unitOfWork.Roles.GetAll();

            return roleItems;
        }

        public RoleReadDTO GetRoleById(int id)
        {
            RoleReadDTO? roleItem = _unitOfWork.Roles.GetById(id);

            return roleItem;
        }

        public RoleReadDTO CreateRole(RoleCreateDTO role)
        {
            RoleReadDTO? createdRole = _unitOfWork.Roles.Create(role);
            _unitOfWork.Commit();
            return createdRole;
        }

        public void UpdateRole(int id, RoleUpdateDTO updateDTO)
        {
            _unitOfWork.Roles.Update(id, updateDTO);
            _unitOfWork.Commit();
        }
    }
}