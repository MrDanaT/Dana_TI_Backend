using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisClub.BL.Entities;

namespace TennisClub.DAL.Configurations
{
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasAlternateKey(i => i.Name);

            builder.Property(i => i.Id).HasColumnType("tinyint").IsRequired();
            builder.Property(i => i.Name).HasColumnType("varchar(20)").IsRequired();

            builder.HasData(
                new League { Id = 0, Name = "Recreatief" },
                new League { Id = 1, Name = "Competitie" },
                new League { Id = 2, Name = "Toptennis" }
            );

            builder.ToTable("tblLeagues");
        }
    }
}
