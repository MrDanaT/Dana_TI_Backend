using System.Collections.Generic;
using TennisClub.Common.Member;

namespace TennisClub.BL.MemberServiceFolder
{
    public interface IMemberService
    {
        IEnumerable<MemberReadDTO> GetAllMembers(string federationNr, string firstName, string lastName,
            string location);

        IEnumerable<MemberReadDTO> GetAllActiveSpelerMembers();

        MemberReadDTO GetMemberById(int id);
        MemberReadDTO CreateMember(MemberCreateDTO member);
        void UpdateMember(int id, MemberUpdateDTO memberToPatch);
        void DeleteMember(int id);

        IEnumerable<MemberReadDTO> GetAllActiveMembers(string federationNr, string firstName, string lastName,
            string location);
    }
}