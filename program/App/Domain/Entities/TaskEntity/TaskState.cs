using Domain.Entities.Abstract;

namespace Domain.Entities.TaskEntity;

public class TaskState : StatusObjectAbstract<TaskState>
{
	public string Description { get; }
	public short Completion { get; }
	public CompletionIndex CompletionIndex { get; }
	private TaskState(
		byte id,
		string name,
		string description,
		short completion) : base(
			id: id,
			name: name
		)
	{
		Description = description;
		CompletionIndex = new CompletionIndex(completion);
		Completion = completion;
	}
	public static readonly TaskState Uncertain = new(
		id: 1,
		name: "Uncertain",
		description: "The task status has not yet been determined.",
		completion: CompletionIndex.Default.Completion
	);
	public static readonly TaskState Completed = new(
		id: 2,
		name: "Completed",
		description: "Task completed.",
		completion: CompletionIndex.Max.Completion
	);
	public static readonly TaskState InProgress = new(
		id: 3,
		name: "In progress",
		description: "The task is in the process of being completed.",
		completion: (CompletionIndex.Max / 3).Completion
	);
	public static readonly TaskState NotCompleted = new(
		id: 4,
		name: "Not completed",
		description: "The task was not completed.",
		completion: CompletionIndex.Min.Completion
	);
}