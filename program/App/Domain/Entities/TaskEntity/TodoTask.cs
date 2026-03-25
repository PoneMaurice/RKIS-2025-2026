using Domain.Interfaces;

namespace Domain.Entities.TaskEntity;

public class TodoTask
{
	private const int MaxNameLength = 255;
	private const int MaxDescriptionLength = 1000;
	private readonly IClock _clock;
	public Guid TaskId { get; private set; }
	public TaskState State { get; private set; }
	public TaskPriority Priority { get; private set; }
	public Guid ProfileId
	{
		get; private set
		{
			if (value == Guid.Empty || value == default)
			{
				throw new ArgumentException("Profile ID cannot be empty.", nameof(value));
			}
			field = value;
		}
	}
	public string Name
	{
		get; private set
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException("Task name cannot be null or empty.", nameof(value));
			}
			if (value.Length > MaxNameLength)
			{
				throw new ArgumentException($"Task name cannot exceed {MaxNameLength} characters.", nameof(value));
			}
			field = value;
		}
	}
	public string? Description
	{
		get; private set
		{
			if (value?.Length > MaxDescriptionLength)
			{
				throw new ArgumentException($"Description cannot exceed {MaxDescriptionLength} characters.", nameof(value));
			}
			field = value;
		}
	}
	public DateTimeOffset CreatedAt { get; private set; }
	public DateTimeOffset? Deadline
	{
		get; private set
		{
			if (value < _clock.Now())
			{
				throw new ArgumentException("Deadline cannot be in the past.", nameof(value));
			}
			field = value;
		}
	}
	public TodoTask(
		IClock clock,
		Guid profileId,
		string name,
		string? description = null,
		DateTimeOffset? deadline = null,
		TaskState? state = null,
		TaskPriority? priority = null)
	{
		_clock = clock;
		state ??= TaskState.Uncertain;
		priority ??= TaskPriority.Medium;
		TaskId = Guid.NewGuid();
		State = state;
		Priority = priority;
		ProfileId = profileId;
		Name = name;
		Description = description;
		CreatedAt = _clock.Now();
		Deadline = deadline;
	}
#pragma warning disable CS8618, CS9264 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
	private TodoTask() { }
#pragma warning restore CS8618, CS9264 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Рассмотрите возможность добавления модификатора "required" или объявления значения, допускающего значение NULL.
	public static TodoTask Restore(
		Guid taskId,
		int stateId,
		int priorityLevel,
		Guid profileId,
		string name,
		string? description,
		DateTimeOffset createdAt,
		DateTimeOffset? deadline) => new()
		{
			TaskId = taskId,
			State = TaskState.ListState.GetById(stateId),
			Priority = TaskPriority.ListPriority.GetByLevel(priorityLevel),
			ProfileId = profileId,
			Name = name,
			Description = description,
			CreatedAt = createdAt,
			Deadline = deadline
		};
	public static TodoTask CreateUpdateObj(
		Guid taskId,
		TaskState state,
		TaskPriority priority,
		string name,
		string? description,
		DateTimeOffset? deadline
	) => new()
	{
		TaskId = taskId,
		State = state,
		Priority = priority,
		Name = name,
		Description = description,
		Deadline = deadline
	};
	public void UpdateName(string name)
	{
		Name = name;
	}
	public void UpdateDescription(string? description)
	{
		Description = description;
	}
	public void UpdateDeadline(DateTimeOffset? deadline)
	{
		Deadline = deadline;
	}
	public void UpdateState(TaskState state)
	{
		State = state;
	}
	public void UpdatePriority(TaskPriority priority)
	{
		Priority = priority;
	}
}