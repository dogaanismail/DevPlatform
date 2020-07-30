using DevPlatform.Entities.Data;
using System;

namespace DevPlatform.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();

        DevPlatformContext GetDbContext();
    }
}
