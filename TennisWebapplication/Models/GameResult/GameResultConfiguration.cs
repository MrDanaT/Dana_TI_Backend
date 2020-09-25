using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisWebapplication.Models
{
    public class GameResultConfiguration : IEntityTypeConfiguration<GameResult>
    {
        public void Configure(EntityTypeBuilder<GameResult> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasAlternateKey(i => new { i.SetNr, i.GameId });

            builder.Property(i => i.Id).HasColumnType("integer");
            builder.Property(i => i.GameId).HasColumnType("integer");
            builder.Property(i => i.SetNr).HasColumnType("tinyint");
            builder.Property(i => i.ScoreTeamMember).HasColumnType("tinyint");
            builder.Property(i => i.ScoreOpponent).HasColumnType("tinyint");

            builder.HasOne(i => i.GameReference)
                .WithMany(g => g.GameResults)
                .HasForeignKey(i => i.GameId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.


            builder.ToTable("Game_results");
        }
    }
}
