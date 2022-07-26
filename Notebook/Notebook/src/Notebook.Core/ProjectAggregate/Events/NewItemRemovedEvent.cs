using Notebook.SharedKernel;

namespace Notebook.Core.ProjectAggregate.Events;
public class NewItemRemovedEvent : DomainEventBase
{
  public int RemovedItemId { get; set; }
  public Project Project { get; set; }

  public NewItemRemovedEvent(Project project,
    int removedItemId)
  {
    Project = project;
    RemovedItemId = removedItemId;
  }
}
