using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisWebapplication.Models
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasAlternateKey(i => i.GameNumber);

            builder.Property(i => i.Id).HasColumnType("integer");
            builder.Property(i => i.GameNumber).HasColumnType("varchar(10)");
            builder.Property(i => i.MemberId).HasColumnType("integer");
            builder.Property(i => i.LeagueId).HasColumnType("tinyint");
            builder.Property(i => i.Date).HasColumnType("date");

            builder.HasOne(i => i.LeagueNavigation)
                .WithMany(l => l.Games)
                .HasForeignKey(i => i.LeagueId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.

            builder.HasOne(i => i.LeagueNavigation)
                .WithMany(l => l.Games)
                .HasForeignKey(i => i.LeagueId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.

            builder.ToTable("Games");
        }
    }
}
