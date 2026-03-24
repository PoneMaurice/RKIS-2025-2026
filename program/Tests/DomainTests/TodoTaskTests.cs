using Domain.Entities.TaskEntity;

namespace DomainTests;

public class TodoTaskTests
{
    [Fact]
    public void New_InvalidProfileId_ThrowsArgumentException()
    {
        Guid empty = Guid.Empty;
        Guid @default = default;
        Assert.Throws<ArgumentException>(() => new TodoTask(
            profileId: empty,
            name: "test"));
        Assert.Throws<ArgumentException>(() => new TodoTask(
            profileId: @default,
            name: "test"));
    }
    [Fact]
    public void UpdateName_InvalidNameLength_ThrowsArgumentException()
    {
        TodoTask testObject = new(
            profileId: Guid.NewGuid(),
            name: "test");
        string newName = new(new Char[256]);
        Assert.Throws<ArgumentException>(() => testObject.UpdateName(newName));
    }
    [Fact]
    public void UpdateName_ValidNameLength_NewName()
    {
        TodoTask testObject = new(
            profileId: Guid.NewGuid(),
            name: "test");
        string newName = "newNameTestObject";
        testObject.UpdateName(newName);
        Assert.Equal(testObject.Name, newName);
    }
    [Fact]
    public void UpdateDescription_InvalidDescriptionLength_ThrowsArgumentException()
    {
        TodoTask testObject = new(
            profileId: Guid.NewGuid(),
            name: "test",
            description: "descriptionTestObject");
        string newDescription = new(new Char[1001]);
        Assert.Throws<ArgumentException>(() => testObject.UpdateDescription(newDescription));
    }
    [Fact]
    public void UpdateDescription_ValidDescriptionLength_NewDescription()
    {
        TodoTask testObject = new(
            profileId: Guid.NewGuid(),
            name: "test",
            description: "descriptionTestObject");
        string newDescription = "newDescriptionTestObject";
        testObject.UpdateDescription(newDescription);
        Assert.Equal(testObject.Description, newDescription);
    }
    [Fact]
    public void UpdateDeadline_InvalidDateOfDeadline_ThrowsArgumentException()
    {
        TodoTask testObject = new(
            profileId: Guid.NewGuid(),
            name: "test",
            deadline: DateTime.Now.AddDays(2));
        DateTime newDeadline = DateTime.Now.AddDays(-2);
        Assert.Throws<ArgumentException>(() => testObject.UpdateDeadline(newDeadline));
    }
    [Fact]
    public void UpdateDeadline_ValidDateOfDeadline_NewDeadline()
    {
        TodoTask testObject = new(
            profileId: Guid.NewGuid(),
            name: "test",
            deadline: DateTime.Now.AddDays(1));
        DateTime newDeadline = DateTime.Now.AddDays(3);
        testObject.UpdateDeadline(newDeadline);
        Assert.Equal(testObject.Deadline, newDeadline);
    }
    [Fact]
    public void UpdateState_ValidState_NewState()
    {
        TodoTask testObject = new(
            profileId: Guid.NewGuid(),
            name: "test",
            state: TaskState.Uncertain);
        TaskState newState = TaskState.Completed;
        testObject.UpdateState(newState);
        Assert.Equal(testObject.State, newState);
    }
    [Fact]
    public void UpdatePriority_ValidPriority_NewPriority()
    {
        TodoTask testObject = new(
            profileId: Guid.NewGuid(),
            name: "test",
            priority: TaskPriority.Low);
        TaskPriority newPriority = TaskPriority.Critical;
        testObject.UpdatePriority(newPriority);
        Assert.Equal(testObject.Priority, newPriority);
    }
}