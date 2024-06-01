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
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Role entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        // throw new NotImplementedException();
        return false;
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