using Terminex.Common.Primitives;

namespace Shared.Kernel.Abstractions
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        Task<Maybe<TEntity>> GetByIdAsync(string id, CancellationToken cl = default);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cl = default);
    }
}