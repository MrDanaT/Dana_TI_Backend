using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasAlternateKey(i => i.Name);

            builder.Property(i => i.Id).HasColumnType("tinyint").IsRequired();
            builder.Property(i => i.Name).HasColumnType("varchar(10)").IsRequired();

            builder.HasData(
                new Gender { Id = 0, Name = "Man" },
                new Gender { Id = 1, Name = "Vrouw" }
            );

            builder.ToTable("tblGenders");
        }
    }
}
