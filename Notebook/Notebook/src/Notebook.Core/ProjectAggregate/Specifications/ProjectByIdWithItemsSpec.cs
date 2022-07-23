using Ardalis.Specification;
using Notebook.Core.ProjectAggregate;

namespace Notebook.Core.ProjectAggregate.Specifications
{
    public class ProjectByIdWithItemsSpec : Specification<Project>, ISingleResultSpecification
    {
        public ProjectByIdWithItemsSpec(int projectId)
        {
            Query
                .Where(project => project.Id == projectId)
                .Include(project => project.Items);
        }
    }
}