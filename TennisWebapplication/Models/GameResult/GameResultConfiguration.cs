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
            builder.Property(i => i.Id).HasColumnType("integer(10)");
            builder.HasAlternateKey(i => new { i.SetNr, i.GameId });
            builder.Property(i => i.GameId).HasColumnType("integer(10)");
            builder.Property(i => i.SetNr).HasColumnType("tinyint(3)");
            builder.Property(i => i.ScoreTeamMember).HasColumnType("tinyint(3)");
            builder.Property(i => i.ScoreOpponent).HasColumnType("tinyint(3)");
            builder.HasOne(i => i.GameId)
                .WithMany(g => g.GameResults)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
