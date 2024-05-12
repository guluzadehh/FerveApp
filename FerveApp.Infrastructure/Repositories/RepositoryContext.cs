using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FerveApp.Infrastructure.Repositories;

public class RepositoryContext : IRepositoryContext
{
    private readonly IConfiguration _config;

    public RepositoryContext(IConfiguration config)
    {
        _config = config;
    }

    public IDbConnection GetConnection(string connectionName = "Default")
    {
        return new SqlConnection(_config.GetConnectionString(connectionName));
    }
}
