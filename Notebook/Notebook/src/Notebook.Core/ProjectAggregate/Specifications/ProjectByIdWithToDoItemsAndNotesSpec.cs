using Ardalis.Specification;

namespace Notebook.Core.ProjectAggregate.Specifications
{
    public class ProjectByIdWithToDoItemsAndNotesSpec : Specification<Project>, ISingleResultSpecification
    {
        public ProjectByIdWithToDoItemsAndNotesSpec(int projectId)
        {
            Query
                .Where(project => project.Id == projectId)
                .Include(project => project.Items)
                .Include(project => project.Notes);
        }
    }
}
