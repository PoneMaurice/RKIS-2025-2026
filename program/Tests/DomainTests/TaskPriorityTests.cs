using Domain.Entities.TaskEntity;

namespace DomainTests;

public class TaskPriorityTests
{
    [Fact]
    public void Escalate_IncreasePriorityByOne_True()
    {
        TaskPriority taskPriority = TaskPriority.Low;
        taskPriority = taskPriority.Escalate();
        Assert.True(taskPriority.Level > TaskPriority.Low.Level);
    }
    [Fact]
    public void Deescalate_lowerPriorityByOne_True()
    {
        TaskPriority taskPriority = TaskPriority.Critical;
        taskPriority = taskPriority.Deescalate();
        Assert.True(taskPriority.Level < TaskPriority.Critical.Level);
    }
    [Fact]
    public void Escalate_IncreaseTheMaximumPriorityByOne_Equal()
    {
        TaskPriority taskPriority = TaskPriority.Critical;
        taskPriority = taskPriority.Escalate();
        Assert.Equal(taskPriority.Level, TaskPriority.Critical.Level);
    }
    [Fact]
    public void Deescalate_lowerTheMinimumPriorityByOne_Equal()
    {
        TaskPriority taskPriority = TaskPriority.Low;
        taskPriority = taskPriority.Deescalate();
        Assert.Equal(taskPriority.Level, TaskPriority.Low.Level);
    }
}