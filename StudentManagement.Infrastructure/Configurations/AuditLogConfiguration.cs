using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Infrastructure.Configurations
{
    public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.UserEmail).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Action).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Entity).HasMaxLength(100).IsRequired();
            builder.Property(a => a.EntityId).IsRequired();
        }
    }
}
