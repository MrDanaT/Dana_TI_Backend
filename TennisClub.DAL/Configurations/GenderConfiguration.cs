using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasIndex(i => i.Name).IsUnique();

            builder.Property(i => i.Id).HasColumnType("integer").UseIdentityColumn();
            builder.Property(i => i.Name).HasColumnType("varchar(10)").IsRequired();

            builder.HasData(
                new Gender {Id = 1, Name = "Man"},
                new Gender {Id = 2, Name = "Vrouw"}
            );

            builder.ToTable("tblGenders");
        }
    }
}