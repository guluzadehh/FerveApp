using System.Data;

namespace FerveApp.Infrastructure.Repositories;

public interface IRepositoryContext
{
    IDbConnection GetConnection(string connectionName = "Default");
}
