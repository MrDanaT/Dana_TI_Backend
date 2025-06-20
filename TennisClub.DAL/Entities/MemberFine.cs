﻿using System;

namespace TennisClub.DAL.Entities
{
    public class MemberFine
    {
        public int Id { get; set; }
        public int FineNumber { get; set; }
        public int MemberId { get; set; }
        public decimal Amount { get; set; }
        public DateTime HandoutDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Member MemberNavigation { get; set; }
    }
}