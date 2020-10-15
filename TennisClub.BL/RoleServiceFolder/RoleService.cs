using System.Collections.Generic;
using TennisClub.Common.Role;
using TennisClub.DAL.Entities;
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
            IEnumerable<RoleReadDTO> roleItems = _unitOfWork.Roles.GetAll();

            return roleItems;
        }

        public RoleReadDTO GetRoleById(byte id)
        {
            RoleReadDTO roleItem = _unitOfWork.Roles.GetById(id);

            return roleItem;
        }

        public RoleReadDTO CreateRole(RoleCreateDTO role)
        {
            var createdRole = _unitOfWork.Roles.Create(role);
            _unitOfWork.Commit();
            return createdRole;
        }

        public RoleUpdateDTO GetUpdateDTOByReadDTO(RoleReadDTO entity)
        {
            return _unitOfWork.Roles.GetUpdateDTOByReadDTO(entity);
        }

        public void UpdateRole(RoleUpdateDTO roleToPatch, RoleReadDTO roleModelFromRepo)
        {
            _unitOfWork.Roles.MapUpdateDTOToReadDTO(roleToPatch, roleModelFromRepo);
            _unitOfWork.Commit();
        }
    }
}
