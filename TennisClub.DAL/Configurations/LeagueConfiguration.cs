﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisClub.DAL.Entities;

namespace TennisClub.DAL.Configurations
{
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasIndex(i => i.Name).IsUnique();

            builder.Property(i => i.Id).HasColumnType("integer").UseIdentityColumn();
            builder.Property(i => i.Name).HasColumnType("varchar(20)").IsRequired();

            builder.HasData(
                new League {Id = 1, Name = "Recreatief"},
                new League {Id = 2, Name = "Competitie"},
                new League {Id = 3, Name = "Toptennis"}
            );

            builder.ToTable("tblLeagues");
        }
    }
}