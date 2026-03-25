using Domain.Entities.TaskEntity;
using Domain.Interfaces;
using Moq;

namespace DomainTests;

public class TodoTaskTests
{
    private readonly Mock<IClock> _clock;
    public TodoTaskTests()
    {
        _clock = new();
        _clock.Setup(c => c.Now())
            .Returns(new DateTimeOffset(new(1999, 9, 9), new(9, 9), new(9, 9, 0)));
    }
    [Fact]
    public void New_InvalidProfileId_ThrowsArgumentException()
    {
        Guid empty = Guid.Empty;
        Guid @default = default;
        Assert.Throws<ArgumentException>(() => new TodoTask(
            clock: _clock.Object,
            profileId: empty,
            name: "test"));
        Assert.Throws<ArgumentException>(() => new TodoTask(
            clock: _clock.Object,
            profileId: @default,
            name: "test"));
    }
    [Fact]
    public void UpdateName_InvalidNameLength_ThrowsArgumentException()
    {
        TodoTask testObject = new(
            clock: _clock.Object,
            profileId: Guid.NewGuid(),
            name: "test");
        string newName = new(new Char[256]);
        Assert.Throws<ArgumentException>(() => testObject.UpdateName(newName));
    }
    [Fact]
    public void UpdateName_ValidNameLength_NewName()
    {
        TodoTask testObject = new(
            clock: _clock.Object,
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
            clock: _clock.Object,
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
            clock: _clock.Object,
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
        var clock = _clock.Object;
        TodoTask testObject = new(
            clock: clock,
            profileId: Guid.NewGuid(),
            name: "test",
            deadline: clock.Now().AddDays(2));
        var newDeadline = clock.Now().AddDays(-2);
        Assert.Throws<ArgumentException>(() => testObject.UpdateDeadline(newDeadline));
    }
    [Fact]
    public void UpdateDeadline_ValidDateOfDeadline_NewDeadline()
    {
        var clock = _clock.Object;
        TodoTask testObject = new(
            clock: clock,
            profileId: Guid.NewGuid(),
            name: "test",
            deadline: clock.Now().AddDays(1));
        var newDeadline = clock.Now().AddDays(3);
        testObject.UpdateDeadline(newDeadline);
        Assert.Equal(testObject.Deadline, newDeadline);
    }
    [Fact]
    public void UpdateState_ValidState_NewState()
    {
        TodoTask testObject = new(
            clock: _clock.Object,
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
            clock: _clock.Object,
            profileId: Guid.NewGuid(),
            name: "test",
            priority: TaskPriority.Low);
        TaskPriority newPriority = TaskPriority.Critical;
        testObject.UpdatePriority(newPriority);
        Assert.Equal(testObject.Priority, newPriority);
    }
}