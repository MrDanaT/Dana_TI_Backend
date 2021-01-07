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

        public override bool Equals(object obj)
        {
            return obj is MemberReadDTO dTO &&
                   FederationNr == dTO.FederationNr &&
                   FirstName == dTO.FirstName &&
                   LastName == dTO.LastName &&
                   BirthDate == dTO.BirthDate &&
                   GenderId == dTO.GenderId &&
                   Address == dTO.Address &&
                   Number == dTO.Number &&
                   Addition == dTO.Addition &&
                   Zipcode == dTO.Zipcode &&
                   City == dTO.City &&
                   PhoneNr == dTO.PhoneNr;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(FederationNr);
            hash.Add(FirstName);
            hash.Add(LastName);
            hash.Add(BirthDate);
            hash.Add(GenderId);
            hash.Add(Address);
            hash.Add(Number);
            hash.Add(Addition);
            hash.Add(Zipcode);
            hash.Add(City);
            hash.Add(PhoneNr);
            return hash.ToHashCode();
        }
    }
}