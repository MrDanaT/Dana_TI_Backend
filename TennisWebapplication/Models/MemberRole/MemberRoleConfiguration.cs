using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisWebapplication.Models
{
    public class MemberRoleConfiguration : IEntityTypeConfiguration<MemberRole>
    {
        public void Configure(EntityTypeBuilder<MemberRole> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasAlternateKey(i => new { i.MemberId, i.RoleId, i.StartDate, i.EndDate});

            builder.Property(i => i.Id).HasColumnName("integer(10)");
            builder.Property(i => i.MemberId).HasColumnType("integer(10)");
            builder.Property(i => i.RoleId).HasColumnType("tinyint(3)");
            builder.Property(i => i.StartDate).HasColumnType("date");
            builder.Property(i => i.EndDate).HasColumnType("date");

            builder.HasOne(i => i.MemberId)
                .WithMany(m => m.MemberRoles)
                .HasForeignKey(i => new { i.RoleId, i.MemberId })
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken

            builder.ToTable("Member_roles");
        }
    }
}
