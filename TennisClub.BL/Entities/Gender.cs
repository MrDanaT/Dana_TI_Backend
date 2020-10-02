using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TennisClub.BL.Entities
{
    public class Gender : BaseEntity
    {
        public Gender()
        {
            Members = new HashSet<Member>();
        }


        public string Name { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}