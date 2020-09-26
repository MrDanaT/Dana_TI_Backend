using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;
using TennisWebapplication.Repositories.MemberFineRepository;

namespace TennisWebapplication.Repositories.MemberRepository
{
    public interface IMemberRepository : ISavable
    {
        IEnumerable<Member> GetCommandByFederationNr(string federationNr);
        IEnumerable<Member> GetCommandByFirstName(string firstName);
        IEnumerable<Member> GetCommandByLastNameAndZipcode(string lastName, string zipcode);
        IEnumerable<Member> GetCommandByCity(string city);
        void CreateMember(Member member);
        void UpdateMember(Member member);
        void DeleteMember(Member member);
    }
}
