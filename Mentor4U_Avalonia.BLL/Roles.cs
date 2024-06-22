// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

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

    public async Task<Role?> CreateAsync(Role role)
    {
        try
        {
            var result = await _data.InsertAsync(role);
            if (!result) return null;
            return await GetByNameAsync(role.RoleName);
        }
        catch (Exception e)
        {
            _logger?.Error($"Module: {nameof(BLL.Roles)}. Method: {nameof(CreateAsync)}. Message:  {e.Message}");
            return null; //TODO: Add exception handling
        }
    }

    public async Task<bool> DeleteAsync(Role role)
    {
        try
        {
            return await _data.DeleteAsync(role.Id);
        }
        catch (Exception e)
        {
            _logger?.Error($"Module: {nameof(BLL.Roles)}. Method: {nameof(DeleteAsync)}. Message:  {e.Message}");
            return false;  //TODO: Add exception handling
        }
    }
}