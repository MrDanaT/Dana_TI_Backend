using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Configurations
{
    public class MemberRoleConfiguration : IEntityTypeConfiguration<MemberRole>
    {
        public void Configure(EntityTypeBuilder<MemberRole> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasIndex(i => new {i.MemberId, i.RoleId, i.StartDate, i.EndDate}).IsUnique();

            builder.Property(i => i.Id).HasColumnType("integer").UseIdentityColumn();
            builder.Property(i => i.MemberId).HasColumnType("integer").IsRequired();
            builder.Property(i => i.RoleId).HasColumnType("integer").IsRequired();
            builder.Property(i => i.StartDate).HasColumnType("date").IsRequired();
            builder.Property(i => i.EndDate)
                .HasColumnType(
                    "date"); // TODO: vragen in combinatie met de unique key. .IsRequired(false); // Is onmogelijk.

            builder.HasOne(i => i.MemberNavigation)
                .WithMany(m => m.MemberRoles)
                .HasForeignKey(i => i.MemberId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken

            builder.HasOne(i => i.RoleNavigation)
                .WithMany(r => r.MemberRoles)
                .HasForeignKey(i => i.RoleId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken

            builder.ToTable("tblMemberRoles");
        }
    }
}