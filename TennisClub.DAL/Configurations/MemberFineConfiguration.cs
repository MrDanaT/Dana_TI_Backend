using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Configurations
{
    public class MemberFineConfiguration : IEntityTypeConfiguration<MemberFine>
    {
        public void Configure(EntityTypeBuilder<MemberFine> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasIndex(i => i.FineNumber).IsUnique(true);

            builder.Property(i => i.Id).HasColumnType("integer").UseIdentityColumn();
            builder.Property(i => i.FineNumber).HasColumnType("integer").IsRequired();
            builder.Property(i => i.MemberId).HasColumnType("integer").IsRequired();
            builder.Property(i => i.Amount).HasColumnType("decimal(7, 2)").IsRequired();
            builder.Property(i => i.HandoutDate).HasColumnType("date").IsRequired();
            builder.Property(i => i.PaymentDate).HasColumnType("date").IsRequired(false);

            builder.HasOne(i => i.MemberNavigation)
                .WithMany(m => m.MemberFines)
                .HasForeignKey(i => i.MemberId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.

            builder.ToTable("tblMemberFines");
        }
    }
}
