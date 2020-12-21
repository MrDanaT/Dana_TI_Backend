using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasIndex(i => i.FederationNr).IsUnique();

            builder.Property(i => i.Id).HasColumnType("integer").UseIdentityColumn();
            builder.Property(i => i.FederationNr).HasColumnType("varchar(10)").IsRequired();
            builder.Property(i => i.FirstName).HasColumnType("varchar(25)").IsRequired();
            builder.Property(i => i.LastName).HasColumnType("varchar(35)").IsRequired();
            builder.Property(i => i.BirthDate).HasColumnType("date").IsRequired();
            builder.Property(i => i.GenderId).HasColumnType("integer").IsRequired();
            builder.Property(i => i.Address).HasColumnType("varchar(70)").IsRequired();
            builder.Property(i => i.Number).HasColumnType("varchar(6)").IsRequired();
            builder.Property(i => i.Addition).HasColumnType("varchar(2)").IsRequired(false);
            builder.Property(i => i.Zipcode).HasColumnType("varchar(6)").IsRequired();
            builder.Property(i => i.City).HasColumnType("varchar(30)").IsRequired();
            builder.Property(i => i.PhoneNr).HasColumnType("varchar(15)").IsRequired(false);
            builder.Property(i => i.Deleted).HasColumnType("bit").HasDefaultValue(false).IsRequired(false);

            builder.HasOne(i => i.GenderNavigation)
                .WithMany(g => g.Members)
                .HasForeignKey(i => i.GenderId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.

            builder.ToTable("tblMembers");
        }
    }
}