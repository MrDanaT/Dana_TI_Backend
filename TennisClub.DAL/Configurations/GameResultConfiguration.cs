using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Configurations
{
    public class GameResultConfiguration : IEntityTypeConfiguration<GameResult>
    {
        public void Configure(EntityTypeBuilder<GameResult> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasIndex(i => new { i.GameId, i.SetNr }).IsUnique(true);

            builder.Property(i => i.Id).HasColumnType("integer").UseIdentityColumn();
            builder.Property(i => i.GameId).HasColumnType("integer").IsRequired();
            builder.Property(i => i.SetNr).HasColumnType("tinyint").IsRequired();
            builder.Property(i => i.ScoreTeamMember).HasColumnType("tinyint").IsRequired();
            builder.Property(i => i.ScoreOpponent).HasColumnType("tinyint").IsRequired();

            builder.HasOne(i => i.GameNavigation)
                .WithMany(g => g.GameResults)
                .HasForeignKey(i => i.GameId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.


            builder.ToTable("tblGameResults");
        }
    }
}
