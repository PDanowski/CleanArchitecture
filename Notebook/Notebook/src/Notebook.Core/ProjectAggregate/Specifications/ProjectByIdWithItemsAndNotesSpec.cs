using Ardalis.Specification;

namespace Notebook.Core.ProjectAggregate.Specifications
{
    public class ProjectByIdWithItemsAndNotesSpec : Specification<Project>, ISingleResultSpecification
    {
        public ProjectByIdWithItemsAndNotesSpec(int projectId)
        {
            Query
                .Where(project => project.Id == projectId)
                .Include(project => project.Items)
                .Include(project => project.Notes);
        }
    }
}
