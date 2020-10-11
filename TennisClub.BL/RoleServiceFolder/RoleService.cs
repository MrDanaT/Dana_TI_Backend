using System.Collections.Generic;
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

        public IEnumerable<Role> GetAllRoles()
        {
            IEnumerable<Role> roleItems = _unitOfWork.Roles.GetAll();

            return roleItems;
        }

        public Role GetRoleById(byte id)
        {
            Role roleItem = _unitOfWork.Roles.GetById(id);

            return roleItem;
        }

        public void CreateRole(Role role)
        {
            _unitOfWork.Roles.Create(role);
            _unitOfWork.Commit();
        }

        public void UpdateRole(Role role)
        {
            _unitOfWork.Commit();
        }
    }
}
