using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.DAL;

public class Roles : ICrud<Role>
{
    private readonly string _connectionString;
    public Roles(string connectionString)
    {
        _connectionString = string.IsNullOrWhiteSpace(connectionString)
            ? throw new EmptyStringException(nameof(connectionString))
            : connectionString;
    }

    public async Task<bool> InsertAsync(Role entity)
    {
        await DbService<Role>.ConnectAsync(_connectionString);
        var sqlRaw = $"""
                      INSERT INTO {DbHelper.TableNames[typeof(Role)]} 
                          ({DbHelper.RoleTablesColumnNames[nameof(Role.RoleName)]}) 
                      VALUES (@RoleName);
                      """;
        var sqlParameters = new { RoleName = entity.RoleName};
        var result = await DbService<Role>.ExecuteNonQueryAsync(sqlRaw, sqlParameters);
        await DbService<Role>.DisconnectAsync();
        
        return result;
    }

    public async Task<bool> UpdateAsync(Role entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        await DbService<Role>.ConnectAsync(_connectionString);
        var sqlRaw = $"""
                      DELETE FROM {DbHelper.TableNames[typeof(Role)]}
                      WHERE  Id = @Id;
                      """;
        var sqlParameters = new { Id = id};
        var result = await DbService<Role>.ExecuteNonQueryAsync(sqlRaw, sqlParameters);
        await DbService<Role>.DisconnectAsync();
        
        return result;
    }

    public async Task<Role?> GetAsync(int id)
    {
        await DbService<Role>.ConnectAsync(_connectionString);
        var role = await DbService<Role>.GetByIdAsync(id);
        await DbService<Role>.DisconnectAsync();
        
        return role;
    }

    public async Task<IEnumerable<Role>?> GetAllAsync()
    {
        await DbService<Role>.ConnectAsync(_connectionString);
        var roles  = await DbService<Role>.GetAllAsync();
        await DbService<Role>.DisconnectAsync();
        
        return roles;
    }
}