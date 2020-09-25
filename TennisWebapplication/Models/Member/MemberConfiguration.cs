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

            builder.Property(i => i.Id).HasColumnName("integer(10)");
            builder.Property(i => i.FederationNr).HasColumnName("varchar(10)");
            builder.Property(i => i.FirstName).HasColumnName("varchar(25)");
            builder.Property(i => i.LastName).HasColumnName("varchar(35)");
            builder.Property(i => i.BirthDate).HasColumnName("date");
            builder.Property(i => i.GenderId).HasColumnName("tinyint(3)");
            builder.Property(i => i.Address).HasColumnName("varchar(70)");
            builder.Property(i => i.Number).HasColumnName("varchar(6)");
            builder.Property(i => i.Addition).HasColumnName("varchar(2)");
            builder.Property(i => i.Zipcode).HasColumnName("varchar(6)");
            builder.Property(i => i.City).HasColumnName("varchar(30)");
            builder.Property(i => i.PhoneNr).HasColumnName("varchar(15)");

            builder.HasOne(i => i.GenderReference)
                .WithMany(g => g.Members)
                .HasForeignKey(i => i.GenderId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.

            builder.ToTable("Member_roles");
        }
    }
}
