using LessonMonitor.DataAccess.MSSQL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LessonMonitor.DataAccess.MSSQL.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title).HasMaxLength(500);
            builder.Property(x => x.Description).HasMaxLength(2000);
            builder.Property(x => x.YouTubeBroadcastId).HasMaxLength(200);

            builder.ToTable("Lessons");

            builder.HasMany(x => x.Homeworks)
               .WithOne(x => x.Lesson)
               .OnDelete(DeleteBehavior.NoAction)
               .HasPrincipalKey(x => x.Id)
               .HasForeignKey(x => x.LessonId)
               .IsRequired(false);

            builder.HasMany(x => x.VisitedLessons)
                .WithOne(x => x.Lesson)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(x => x.LessonId)
                .IsRequired();
        }
    }
}
