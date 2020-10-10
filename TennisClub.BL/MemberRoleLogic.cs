using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TennisClub.DAL.Entities;
using TennisClub.DAL.Repositories.MemberRepository;
using TennisClub.DAL.Repositories.MemberRoleRepository;

namespace TennisClub.BL
{
    public class MemberRoleLogic
    {
        private readonly IMemberRoleRepository _repo;
        private readonly IMemberRepository _memberRepo;

        public MemberRoleLogic(IMemberRoleRepository repo, IMemberRepository memberRepo)
        {
            _repo = repo;
            _memberRepo = memberRepo;
        }

        public IEnumerable<MemberRole> GetAllMemberRoles()
        {
            IEnumerable<MemberRole> memberRoleItems = _repo.GetAll();

            return memberRoleItems;
        }

        public MemberRole GetMemberRoleById(int id)
        {
            MemberRole memberRoleItem = _repo.GetById(id);

            return memberRoleItem;
        }

        public void CreateMemberRole(MemberRole memberRole)
        {
            _repo.Create(memberRole);
            _repo.SaveChanges();
        }

        public void PartialMemberRoleUpdate(MemberRole memberRole)
        {
            _repo.Update(memberRole);
            _repo.SaveChanges();
        }

        public IEnumerable<Role> GetRolesByMemberId(int id)
        {
            // TODO: Dit nakijken samen met repository.
            Member memberFromRepo = _memberRepo.GetById(id);
            IEnumerable<Role> roleItems = _repo.GetRolesByMember(memberFromRepo).ToList();

            return roleItems;
        }

        public IEnumerable<Member> GetMembersByRoles(List<Role> roles)
        {
            // TODO: Dit nakijken samen met repository.
            IEnumerable<Member> memberItems = _repo.GetMembersByRoles(roles);

            return memberItems;
        }
    }
}
