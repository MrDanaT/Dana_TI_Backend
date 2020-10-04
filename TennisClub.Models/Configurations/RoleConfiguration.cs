using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasAlternateKey(i => i.Name);

            builder.Property(i => i.Id).HasColumnType("tinyint").IsRequired();
            builder.Property(i => i.Name).HasColumnType("varchar(20)").IsRequired();

            builder.HasData(
                new Role { Id = 0, Name = "Voorzitter" },
                new Role { Id = 1, Name = "Bestuurslid" },
                new Role { Id = 2, Name = "Secretaris" },
                new Role { Id = 3, Name = "Penningmeester" },
                new Role { Id = 4, Name = "Speler" }
            );

            builder.ToTable("tblRoles");
        }
    }
}
