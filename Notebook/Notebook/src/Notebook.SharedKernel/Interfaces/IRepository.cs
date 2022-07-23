using Ardalis.Specification;

namespace Notebook.SharedKernel.Interfaces
{
    // from Ardalis.Specification
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}