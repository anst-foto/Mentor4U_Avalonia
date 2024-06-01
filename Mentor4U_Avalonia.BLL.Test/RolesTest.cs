using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.BLL.Test;

public class RolesTest
{
    private const string ConnectionString = "Host=localhost;Port=5432;Database=mentor_db;User ID=postgres;Password=1234;Pooling=true;SearchPath=test;";
    
    [Fact]
    public async Task GetByNameAsync_PositiveTest()
    {
        var roles = new Roles(new DAL.Roles(ConnectionString));
        var actual = await roles.GetByNameAsync("admin");
        
        var expected = new Role()
        {
            Id = 1,
            RoleName  =  "admin"
        };
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetByNameAsync_NegativeTest()
    {
        var roles = new Roles(new DAL.Roles(ConnectionString));
        var actual = await roles.GetByNameAsync("admin1");
        
        Assert.Null(actual);
    }
}