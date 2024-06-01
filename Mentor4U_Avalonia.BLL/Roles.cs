using Mentor4U_Avalonia.DAL;
using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.BLL;

public class Roles
{
    private readonly ICrud<Role> _data;

    public Roles(ICrud<Role> data)
    {
        _data = data;
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _data.GetAsync(id);
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        return (await _data.GetAllAsync() ?? Array.Empty<Role>())
            .SingleOrDefault(r => r.RoleName == name);
    }

    public async Task<IEnumerable<Role>?> GetAllAsync()
    {
        return await _data.GetAllAsync();
    }
}