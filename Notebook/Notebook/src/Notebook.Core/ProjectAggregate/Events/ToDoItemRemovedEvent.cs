using Notebook.SharedKernel;

namespace Notebook.Core.ProjectAggregate.Events;
public class ToDoItemRemovedEvent : DomainEventBase
{
  public int RemovedItemId { get; set; }
  public Project Project { get; set; }

  public ToDoItemRemovedEvent(Project project,
    int removedItemId)
  {
    Project = project;
    RemovedItemId = removedItemId;
  }
}
