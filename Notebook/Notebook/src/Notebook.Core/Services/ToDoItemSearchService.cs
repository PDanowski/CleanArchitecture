﻿using Ardalis.Result;
using Notebook.Core.Interfaces;
using Notebook.Core.ProjectAggregate;
using Notebook.Core.ProjectAggregate.Specifications;
using Notebook.SharedKernel.Interfaces;

namespace Notebook.Core.Services
{
    public class ToDoItemSearchService : IToDoItemSearchService
    {
        private readonly IRepository<Project> _repository;

        public ToDoItemSearchService(IRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task<Result<List<ToDoItem>>> GetAllIncompleteItemsAsync(int projectId, string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                var errors = new List<ValidationError>();
                errors.Add(new ValidationError()
                {
                    Identifier = nameof(searchString),
                    ErrorMessage = $"{nameof(searchString)} is required."
                });
                return Result<List<ToDoItem>>.Invalid(errors);
            }

            var projectSpec = new ProjectByIdWithToDoItemsAndNotesSpec(projectId);
            var project = await _repository.GetBySpecAsync(projectSpec);

            // TODO: Optionally use Ardalis.GuardClauses Guard.Against.NotFound and catch
            if (project == null) return Result<List<ToDoItem>>.NotFound();

            var incompleteSpec = new IncompleteToDoItemsSearchSpec(searchString);

            try
            {
                var items = incompleteSpec.Evaluate(project.Items).ToList();

                return new Result<List<ToDoItem>>(items);
            }
            catch (Exception ex)
            {
                // TODO: Log details here
                return Result<List<ToDoItem>>.Error(new[] { ex.Message });
            }
        }

        public async Task<Result<ToDoItem>> GetNextIncompleteItemAsync(int projectId)
        {
            var projectSpec = new ProjectByIdWithToDoItemsAndNotesSpec(projectId);
            var project = await _repository.GetBySpecAsync(projectSpec);
            if (project == null)
            {
                return Result<ToDoItem>.NotFound();
            }

            var incompleteSpec = new IncompleteToDoItemsSpec();

            var items = incompleteSpec.Evaluate(project.Items).ToList();

            if (!items.Any())
            {
                return Result<ToDoItem>.NotFound();
            }

            return new Result<ToDoItem>(items.First());
        }
    }
}