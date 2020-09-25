using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TennisWebapplication.Models
{
    public class Gender
    {
        public Gender()
        {
            Members = new HashSet<Member>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }
}