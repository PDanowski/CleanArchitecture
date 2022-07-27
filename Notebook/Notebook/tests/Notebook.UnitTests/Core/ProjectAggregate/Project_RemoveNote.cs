using Notebook.Core.ProjectAggregate;
using Xunit;

namespace Notebook.UnitTests.Core.ProjectAggregate;
public class Project_RemoveNote
{
  private Project _testProject = new Project("some name", PriorityStatus.Backlog);
  private Note _testNote = new Note
  {
    Id = 123,
    Title = "title",
    Content = "content"
  };

  public Project_RemoveNote()
  {
    _testProject.AddNote(_testNote);
  }

  [Fact]
  public void RemoveToDoItemFromList()
  {
    _testProject.RemoveNote(_testNote.Id);

    Assert.DoesNotContain(_testNote, _testProject.Notes);
  }
}
