using System.Collections.Generic;
using TennisClub.Common.MemberFine;
using TennisClub.DAL.Entities;

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
