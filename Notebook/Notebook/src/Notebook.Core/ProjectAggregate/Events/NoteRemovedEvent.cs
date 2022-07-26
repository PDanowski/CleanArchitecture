using Notebook.SharedKernel;

namespace Notebook.Core.ProjectAggregate.Events;
public class NoteRemovedEvent : DomainEventBase
{
  public int RemovedNoteId { get; set; }
  public Project Project { get; set; }

  public NoteRemovedEvent(Project project,
    int noteId)
  {
    Project = project;
    RemovedNoteId = noteId;
  }
}
