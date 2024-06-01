using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.DAL.Test;

public class RolesTest
{
    private const string ConnectionString = "Host=localhost;Port=5432;Database=mentor_db;User ID=postgres;Password=1234;Pooling=true;SearchPath=test;";
    
    [Fact]
    public async Task GetAsync_PositiveTest()
    {
        var roles = new Roles(ConnectionString);
        var actual = await roles.GetAsync(1);

        var expected = new Role()
        {
            Id = 1,
            RoleName = "admin"
        };
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetAsync_NegativeTest()
    {
        var roles = new Roles(ConnectionString);
        var actual = await roles.GetAsync(0);
        
        Assert.Null(actual);
    }
}