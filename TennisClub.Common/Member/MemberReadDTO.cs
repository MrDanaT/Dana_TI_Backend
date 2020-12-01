using System;

namespace TennisClub.Common.Member
{
    public class MemberReadDTO : BaseReadDTO
    {
        public string FederationNr { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public DateTime BirthDate { get; set; }
        public int GenderId { get; set; }
        public string GenderName { get; set; }

        public string Address { get; set; }
        public string Number { get; set; }
        public string Addition { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string PhoneNr { get; set; }
        public bool Deleted { get; set; }
    }
}
