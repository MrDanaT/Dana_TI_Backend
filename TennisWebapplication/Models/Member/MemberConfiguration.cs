using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisWebapplication.Models
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasAlternateKey(i => i.FederationNr);

            builder.Property(i => i.Id).HasColumnType("integer(10)");
            builder.Property(i => i.FederationNr).HasColumnType("varchar(10)");
            builder.Property(i => i.FirstName).HasColumnType("varchar(25)");
            builder.Property(i => i.LastName).HasColumnType("varchar(35)");
            builder.Property(i => i.BirthDate).HasColumnType("date");
            builder.Property(i => i.GenderId).HasColumnType("tinyint(3)");
            builder.Property(i => i.Address).HasColumnType("varchar(70)");
            builder.Property(i => i.Number).HasColumnType("varchar(6)");
            builder.Property(i => i.Addition).HasColumnType("varchar(2)");
            builder.Property(i => i.Zipcode).HasColumnType("varchar(6)");
            builder.Property(i => i.City).HasColumnType("varchar(30)");
            builder.Property(i => i.PhoneNr).HasColumnType("varchar(15)");

            builder.HasOne(i => i.GenderReference)
                .WithMany(g => g.Members)
                .HasForeignKey(i => i.GenderId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.

            builder.ToTable("Member_roles");
        }
    }
}
