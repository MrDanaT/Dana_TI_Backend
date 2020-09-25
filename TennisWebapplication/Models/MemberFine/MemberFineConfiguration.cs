using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisWebapplication.Models
{
    public class MemberFineConfiguration : IEntityTypeConfiguration<MemberFine>
    {
        public void Configure(EntityTypeBuilder<MemberFine> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasAlternateKey(i => i.FineNumber);

            builder.Property(i => i.Id).HasColumnType("tinyint");
            builder.Property(i => i.FineNumber).HasColumnType("integer");
            builder.Property(i => i.MemberId).HasColumnType("integer");
            builder.Property(i => i.Amount).HasColumnType("decimal(7, 2)");
            builder.Property(i => i.HandoutDate).HasColumnType("date");
            builder.Property(i => i.PaymentDate).HasColumnType("date");

            builder.HasOne(i => i.MemberNavigation)
                .WithMany(m => m.MemberFines)
                .HasForeignKey(i => i.MemberId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.

            builder.ToTable("Member_fines");
        }
    }
}
