using Domain.Entities.TaskEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Config;

public class TaskStateConfig : IEntityTypeConfiguration<TaskState>
{
	public void Configure(EntityTypeBuilder<TaskState> builder)
	{
		builder.ToTable("task_states");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasColumnName("state_id")
            .IsRequired();

        builder.Property(s => s.Name)
            .HasColumnName("name")
            .IsRequired();

        builder.Property(s => s.Description)
            .HasColumnName("description");

        builder.Property(s => s.Completion)
            .HasColumnName("completion")
            .IsRequired();

        builder.Ignore(s => s.CompletionIndex);

        builder.HasIndex(s => s.Id)
            .IsUnique();
        builder.HasIndex(s => s.Name);
        builder.HasIndex(s => s.Completion);

        builder.HasData(TaskState.All);
	}
}