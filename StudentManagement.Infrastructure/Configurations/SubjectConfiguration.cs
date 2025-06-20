using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Infrastructure.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Code).IsRequired().HasMaxLength(10);
            builder.Property(s => s.Credits).IsRequired();
            builder.HasMany(s => s.Enrollments)
                   .WithOne(e => e.Subject)
                   .HasForeignKey(e => e.SubjectId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
