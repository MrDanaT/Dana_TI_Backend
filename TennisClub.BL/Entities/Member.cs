
using System;
using System.Collections.Generic;
using TennisClub.BL.Entities.Common;

namespace TennisClub.BL.Entities
{
    public class Member : BaseEntity
    {
        public Member()
        {
            MemberRoles = new HashSet<MemberRole>();
            MemberFines = new HashSet<MemberFine>();
            Games = new HashSet<Game>();
        }

        public string FederationNr { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public byte GenderId { get; set; }

        public string Address { get; set; }
        public string Number { get; set; }
        public string Addition { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string PhoneNr { get; set; }

        public virtual ICollection<MemberRole> MemberRoles { get; set; }
        public virtual ICollection<MemberFine> MemberFines { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual Gender GenderNavigation { get; set; }

    }
}