﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasKey(i => i.Id);

            builder.HasIndex(i => i.GameNumber).IsUnique();

            builder.Property(i => i.Id).HasColumnType("integer").UseIdentityColumn();
            builder.Property(i => i.GameNumber).HasColumnType("varchar(10)").IsRequired();
            builder.Property(i => i.MemberId).HasColumnType("integer").IsRequired();
            builder.Property(i => i.LeagueId).HasColumnType("integer").IsRequired();
            builder.Property(i => i.Date).HasColumnType("date").IsRequired();

            builder.HasOne(i => i.LeagueNavigation)
                .WithMany(l => l.Games)
                .HasForeignKey(i => i.LeagueId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.

            builder.HasOne(i => i.MemberNavigation)
                .WithMany(l => l.Games)
                .HasForeignKey(i => i.MemberId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.

            builder.ToTable("tblGames");
        }
    }
}