using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.DAL;

public class Persons : ICrud<Person>
{
    private readonly string _connectionString;
    public Persons(string connectionString)
    {
        _connectionString = string.IsNullOrWhiteSpace(connectionString)
            ? throw new EmptyStringException(nameof(connectionString))
            : connectionString;
    }

    public async Task<bool> InsertAsync(Person entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Person entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Person?> GetAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Person>?> GetAllAsync()
    {
        await DbService<Person>.ConnectAsync(_connectionString);
        var persons = await DbService<Person>.GetAllAsync();
        await DbService<Person>.DisconnectAsync();

        return persons;
    }
}
