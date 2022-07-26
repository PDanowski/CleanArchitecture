using Notebook.SharedKernel;

namespace Notebook.Core.ProjectAggregate.Events;

public class NewToDoItemAddedEvent : DomainEventBase
{
  public ToDoItem NewItem { get; set; }
  public Project Project { get; set; }

  public NewToDoItemAddedEvent(Project project,
    ToDoItem newItem)
  {
    Project = project;
    NewItem = newItem;
  }
}
