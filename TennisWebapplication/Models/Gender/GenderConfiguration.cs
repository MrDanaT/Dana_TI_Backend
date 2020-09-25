using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisWebapplication.Models
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasAlternateKey(i => i.Name);

            builder.Property(i => i.Id).HasColumnName("tinyint(3)");
            builder.Property(i => i.Name).HasColumnType("varchar(10)");

            builder.HasMany(i => i.Members)
                .WithOne(m => m.GenderId)
                .OnDelete(DeleteBehavior.Cascade); // TODO: nakijken.

            builder.ToTable("Genders");
        }
    }
}
