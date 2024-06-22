using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.BLL.Test;

public class RolesTest
{
    private const string ConnectionString = "Host=localhost;Port=5432;Database=mentor_db;User ID=postgres;Password=1234;Pooling=true;SearchPath=test;";

    private const string RoleName = "guest";
    private const string RoleNameBad = "admin";
    
    
    [Fact]
    public async Task GetByIdAsync_PositiveTest()
    {
        //TODO Implement test
        Assert.True(true);
    }
    
    [Fact]
    public async Task GetByIdAsync_NegativeTest()
    {
        //TODO Implement test
        Assert.False(false);
    }
    
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

    [Fact]
    public async Task GetAllAsync_PositiveTest()
    {
        //TODO Implement test
        Assert.True(true);
    }

    [Fact]
    public async Task GetAllAsync_NegativeTest()
    {
        //TODO Implement test
        Assert.False(false);
    }

    [Fact]
    public async Task CreateAndDeleteAsync_PositiveTest()
    {
        var roles = new Roles(new DAL.Roles(ConnectionString));
        
        var role = new Role() { RoleName = RoleName };
        var roleNew = await roles.CreateAsync(role);
        Assert.NotNull(roleNew);
        
        var result  = await roles.DeleteAsync(roleNew);
        Assert.True(result);
    }

    [Fact]
    public async Task CreateAsync_NegativeTest()
    {
        var roles = new Roles(new DAL.Roles(ConnectionString));
        
        var role = new Role() { RoleName = RoleNameBad };
        var roleNew = await roles.CreateAsync(role);
        Assert.Null(roleNew);
    }
}