using AFS.Core.Entities;
using System;

namespace AFS.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IEntityRepository<T> GetRepository<T>() where T : class, IEntity, new();
        bool BeginNewTransaction();
        bool RollBackTransaction();
        bool SaveChanges();
        bool Commit();
    }
}
