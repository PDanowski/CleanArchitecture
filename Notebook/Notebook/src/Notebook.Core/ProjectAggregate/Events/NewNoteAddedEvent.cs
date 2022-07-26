using Notebook.SharedKernel;

namespace Notebook.Core.ProjectAggregate.Events;
public class NewNoteAddedEvent : DomainEventBase
{
  public Note NewNote { get; set; }
  public Project Project { get; set; }

  public NewNoteAddedEvent(Project project, Note newNote)
  {
    Project = project;
    NewNote = newNote;
  }
}
