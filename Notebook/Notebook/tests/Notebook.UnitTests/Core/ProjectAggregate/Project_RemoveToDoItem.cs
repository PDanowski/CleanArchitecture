using Notebook.Core.ProjectAggregate;
using Xunit;

namespace Notebook.UnitTests.Core.ProjectAggregate;
public class Project_RemoveToDoItem
{
  private Project _testProject = new Project("some name", PriorityStatus.Backlog);
  private ToDoItem _testItem = new ToDoItem { Id = 123, Title = "title", Description = "description" };

  public Project_RemoveToDoItem()
  {
    _testProject.AddToDoItem(_testItem);
  }

  [Fact]
  public void RemoveToDoItemFromList()
  {
    _testProject.RemoveToDoItem(_testItem.Id);

    Assert.DoesNotContain(_testItem, _testProject.Items);
  }
}
