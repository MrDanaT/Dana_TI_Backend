using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TennisWebapplication.Models;

namespace TennisWebapplication.Repositories.MemberRepository
{
    public class MemberRepository : IMemberRepository
    {
        public void CreateMember(Member member)
        {
            throw new NotImplementedException();
        }

        public void DeleteMember(Member member)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetCommandByCity(string city)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetCommandByFederationNr(string federationNr)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetCommandByFirstName(string firstName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Member> GetCommandByLastNameAndZipcode(string lastName, string zipcode)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateMember(Member member)
        {
            throw new NotImplementedException();
        }
    }
}
