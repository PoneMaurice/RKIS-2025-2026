using Domain.Entities.TaskEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;

public class TaskPriorityConfig : IEntityTypeConfiguration<TaskPriority>
{
	public void Configure(EntityTypeBuilder<TaskPriority> builder)
	{
		builder.ToTable("task_priorities");

        builder.HasKey(p => p.Level);

        builder.Property(p => p.Level)
            .HasColumnName("level")
            .IsRequired();

        builder.Property(p => p.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Ignore(s => s.Id);

        builder.HasIndex(s => s.Level)
            .IsUnique();
        builder.HasIndex(s => s.Name)
            .IsUnique();

        builder.HasData(TaskPriority.All);
	}
}