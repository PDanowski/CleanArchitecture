using Ardalis.Specification;

namespace Notebook.Core.ProjectAggregate.Specifications
{
    public class IncompleteToDoItemsSearchSpec : Specification<ToDoItem>
    {
        public IncompleteToDoItemsSearchSpec(string searchString)
        {
            Query
                .Where(item => !item.IsDone &&
                (item.Title.Contains(searchString) ||
                item.Description.Contains(searchString)));
        }
    }
}
