using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TennisWebapplication.Models
{
    public class Member
    {
        public Member()
        {
            MemberRoles = new HashSet<MemberRole>();
            MemberFines = new HashSet<MemberFine>();
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public string FederationNr { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }

        public string Address { get; set; }
        public string Number { get; set; }
        public string Addition { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string PhoneNr { get; set; }

        public virtual ICollection<MemberRole> MemberRoles { get; set; }
        public virtual ICollection<MemberFine> MemberFines { get; set; }
        public virtual ICollection<Game> Games { get; set; }
        public virtual Gender GenderReference { get; set; }

    }
}