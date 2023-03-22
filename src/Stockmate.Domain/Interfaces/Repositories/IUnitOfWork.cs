namespace Stockmate.Domain.Entities;

public interface IUnitOfWork : IDisposable
{
    Task<int> CommitAsync();
}
