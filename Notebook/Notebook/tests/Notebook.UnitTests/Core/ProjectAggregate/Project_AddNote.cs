using Notebook.Core.ProjectAggregate;
using Xunit;

namespace Notebook.UnitTests.Core.ProjectAggregate;
public class Project_AddNote
{
  private Project _testProject = new Project("some name", PriorityStatus.Backlog);

  [Fact]
  public void AddNoteToList()
  {
    var _testNote = new Note
    {
      Title = "title",
      Content = "content",
      Url = "http://google.com"
    };

    _testProject.AddNote(_testNote);

    Assert.Contains(_testNote, _testProject.Notes);
  }

  [Fact]
  public void ThrowsExceptionGivenNullItem()
  {
#nullable disable
    Action action = () => _testProject.AddNote(null);
#nullable enable

    var ex = Assert.Throws<ArgumentNullException>(action);
    Assert.Equal("newNote", ex.ParamName);
  }
}
