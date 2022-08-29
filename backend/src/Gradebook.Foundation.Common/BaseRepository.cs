using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Gradebook.Foundation.Common;

public abstract class BaseRepository<T> : BaseRepository where T : DbContext
{
    protected T Context { get; }

    protected BaseRepository(T context) : base(context.Database.GetDbConnection().ConnectionString)
    {
        Context = context;
    }
    public override Task SaveChangesAsync()
        => Context.SaveChangesAsync();
    
    public override void SaveChanges()
        => Context.SaveChanges();
    
}

public abstract class BaseRepository : IBaseRepository
{
    protected string ConnectionString { get; set; }

    protected BaseRepository(string connectionString)
    {
        ConnectionString = connectionString;
    }
    protected async Task<SqlConnection> GetOpenConnectionAsync()
    {
        var connection = new SqlConnection(ConnectionString);
        await connection.OpenAsync();
        return connection;
    }
    protected SqlConnection GetOpenConnection()
    {
        var connection = new SqlConnection(ConnectionString);
        connection.Open();
        return connection;
    }

    public abstract void SaveChanges();

    public abstract Task SaveChangesAsync();
}

public interface IBaseRepository{
    void SaveChanges();
    Task SaveChangesAsync();
}
