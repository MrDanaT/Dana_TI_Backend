using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using TennisClub.Common.Member;
using TennisClub.DAL.Entities;

namespace TennisClub.BL.MemberServiceFolder
{
    public interface IMemberService
    {
        IEnumerable<MemberReadDTO> GetAllMembers();
        MemberReadDTO GetMemberById(int id);
        MemberReadDTO CreateMember(MemberCreateDTO member);
        void UpdateMember(MemberUpdateDTO memberToPatch, MemberReadDTO memberModelFromRepo);
        void DeleteMember(MemberReadDTO member);
        IEnumerable<MemberReadDTO> GetAllActiveMembers();
        MemberUpdateDTO GetUpdateDTOByReadDTO(MemberReadDTO entity);
    }
}
