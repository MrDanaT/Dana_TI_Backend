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

            builder.Property(i => i.GameId).HasColumnType("integer").IsRequired();
            builder.Property(i => i.SetNr).HasColumnType("tinyint").IsRequired();
            builder.Property(i => i.ScoreTeamMember).HasColumnType("tinyint").IsRequired();
            builder.Property(i => i.ScoreOpponent).HasColumnType("tinyint").IsRequired();

            builder.HasOne(i => i.GameNavigation)
                .WithMany(g => g.GameResults)
                .HasForeignKey(i => i.GameId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.


            builder.ToTable("Game_results");
        }
    }
}
