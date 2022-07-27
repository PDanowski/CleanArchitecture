using Notebook.Core.ProjectAggregate;
using Xunit;

namespace Notebook.UnitTests.Core.ProjectAggregate;

public class Project_AddToDoItem
{
  private Project _testProject = new Project("some name", PriorityStatus.Backlog);

  [Fact]
  public void AddToDoItemToList()
  {
    var _testItem = new ToDoItem
    {
      Title = "title",
      Description = "description"
    };

    _testProject.AddToDoItem(_testItem);

    Assert.Contains(_testItem, _testProject.Items);
  }

  [Fact]
  public void ThrowsExceptionGivenNullItem()
  {
#nullable disable
    Action action = () => _testProject.AddToDoItem(null);
#nullable enable

    var ex = Assert.Throws<ArgumentNullException>(action);
    Assert.Equal("newToDoItem", ex.ParamName);
  }
}
