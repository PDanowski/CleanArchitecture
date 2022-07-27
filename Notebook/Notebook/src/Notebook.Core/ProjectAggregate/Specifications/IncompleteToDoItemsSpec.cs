using Ardalis.Specification;

namespace Notebook.Core.ProjectAggregate.Specifications
{
    public class IncompleteToDoItemsSpec : Specification<ToDoItem>
    {
        public IncompleteToDoItemsSpec()
        {
            Query.Where(item => !item.IsDone);
        }
    }
}
