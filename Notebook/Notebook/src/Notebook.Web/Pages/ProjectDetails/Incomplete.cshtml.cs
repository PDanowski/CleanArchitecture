using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Notebook.Core.ProjectAggregate;
using Notebook.Core.ProjectAggregate.Specifications;
using Notebook.SharedKernel.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notebook.Web.Pages.ToDoRazorPage
{
    public class IncompleteModel : PageModel
    {
        private readonly IRepository<Project> _repository;

        [BindProperty(SupportsGet = true)]
        public int ProjectId { get; set; } = 1;

        public List<ToDoItem>? ToDoItems { get; set; }

        public IncompleteModel(IRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task OnGetAsync(CancellationToken cancellationToken)
        {
            var projectSpec = new ProjectByIdWithToDoItemsAndNotesSpec(ProjectId);
            var project = await _repository.FirstOrDefaultAsync(projectSpec, cancellationToken);
            if (project == null)
            {
                return;
            }

            var spec = new IncompleteToDoItemsSpec();
            ToDoItems = spec.Evaluate(project.Items).ToList();
        }
    }
}
