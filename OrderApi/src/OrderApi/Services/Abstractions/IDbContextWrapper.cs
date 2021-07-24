using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace OrderApi.Services.Abstractions
{
    public interface IDbContextWrapper<T>
        where T : DbContext
    {
        T DbContext { get; }
        IDbContextTransaction BeginTransaction();
    }
}
