using Logger;
using Logger.File;
using Mentor4U_Avalonia.DAL;
using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.BLL;

public class Persons
{
    private readonly ICrud<Person> _data;
    private readonly ILogger? _logger;

    public Persons(ICrud<Person> data)
    {
        _data = data;

        _logger = new LogToFile();
    }

    public async Task<Person?> GetByLoginAsync(string login)
    {
        try
        {
            return (await _data.GetAllAsync() ?? Array.Empty<Person>())
                .SingleOrDefault(p => p.Login == login);
        }
        catch (Exception e)
        {
            _logger?.Error($"Module: {nameof(Roles)}. Method: {nameof(GetByLoginAsync)}. Message:  {e.Message}");
            throw;
        }
    }
}
