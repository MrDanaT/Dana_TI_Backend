using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TennisWebapplication.Models
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasAlternateKey(i => i.Name);
            builder.Property(i => i.Id).HasColumnName("tinyint(3)");
            builder.Property(i => i.Name).HasColumnType("varchar(20)");
        }
    }
}
