using System.Collections.Generic;
using TennisClub.Common.MemberFine;

namespace TennisClub.BL.MemberFineServiceFolder
{
    public interface IMemberFineService
    {
        IEnumerable<MemberFineReadDTO> GetAllMemberFines();
        MemberFineReadDTO GetMemberFineById(int id);

        MemberFineReadDTO CreateMemberFine(MemberFineCreateDTO memberFineCreateDto);

        void UpdateMemberFine(MemberFineUpdateDTO modelFineToPatch, MemberFineReadDTO memberFineModelFromRepo);
        IEnumerable<MemberFineReadDTO> GetMemberFinesByMemberId(int id);
        MemberFineUpdateDTO GetUpdateDTOByReadDTO(MemberFineReadDTO entity);
    }
}
