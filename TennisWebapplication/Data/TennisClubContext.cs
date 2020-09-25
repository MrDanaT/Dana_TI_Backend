using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TennisWebapplication.Models
{
    public class TennisClubContext : DbContext
    {
        public TennisClubContext(DbContextOptions<TennisClubContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<GameResult> GameResults { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberFine> MemberFines { get; set; }
        public DbSet<MemberRole> MemberRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new GameResultConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new LeagueConfiguration());
            modelBuilder.ApplyConfiguration(new MemberConfiguration());
            modelBuilder.ApplyConfiguration(new MemberFineConfiguration());
            modelBuilder.ApplyConfiguration(new MemberRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}