using Logger;
using Logger.File;
using Mentor4U_Avalonia.DAL;
using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.BLL;

public class Roles
{
    private readonly ILogger? _logger;
    
    private readonly ICrud<Role> _data;

    public Roles(ICrud<Role> data)
    {
        _data = data;

        _logger = new LogToFile();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        try
        {
            return await _data.GetAsync(id);
        }
        catch (Exception e)
        {
            _logger?.Error($"Module: {nameof(BLL.Roles)}. Method: {nameof(GetByIdAsync)}. Message:  {e.Message}");
            throw;
        }
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        try
        {
            return (await _data.GetAllAsync() ?? Array.Empty<Role>())
                .SingleOrDefault(r => r.RoleName == name);
        }
        catch (Exception e)
        {
            _logger?.Error($"Module: {nameof(BLL.Roles)}. Method: {nameof(GetByNameAsync)}. Message:  {e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Role>?> GetAllAsync()
    {
        try
        {
            return await _data.GetAllAsync();
        }
        catch (Exception e)
        {
            _logger?.Error($"Module: {nameof(BLL.Roles)}. Method: {nameof(GetAllAsync)}. Message:  {e.Message}");
            throw;
        }
    }
}