using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisWebapplication.Models
{
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasAlternateKey(i => i.Name);

            builder.Property(i => i.Id).HasColumnType("tinyint(3)").IsRequired();
            builder.Property(i => i.Name).HasColumnType("varchar(20)").IsRequired();

            builder.HasData(
                new League { Id = 1, Name = "Recreatief" },
                new League { Id = 2, Name = "Competitie" },
                new League { Id = 3, Name = "Toptennis" }
            );

            builder.ToTable("tblLeagues");
        }
    }
}
