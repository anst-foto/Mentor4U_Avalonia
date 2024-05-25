using Dapper;
using Mentor4U_Avalonia.Models;
using Npgsql;

namespace Mentor4U_Avalonia.DAL;

public static class DbService<TEntity> where TEntity : IModel
{
    private static NpgsqlConnection? _connection;

    public static async Task ConnectAsync(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString)) throw new EmptyStringException(nameof(connectionString));
        
        //TODO Добавить try {} catch {}
        _connection = new NpgsqlConnection(connectionString);
        await _connection.OpenAsync();
    }

    public static async Task<IEnumerable<TEntity>> GetAllAsync(string sql)
    {
        //TODO Добавить try {} catch {}
        return await _connection?.QueryAsync<TEntity>(sql);
    }

    public static async Task<TEntity?> GetByIdAsync(string sql)
    {
        //TODO Добавить try {} catch {}
        return await _connection?.QuerySingleOrDefaultAsync<TEntity>(sql);
    }
    
    public static async Task DisconnectAsync()
    {
        if (_connection == null) return;
        
        await _connection.CloseAsync();
        await _connection.DisposeAsync();
    }
}