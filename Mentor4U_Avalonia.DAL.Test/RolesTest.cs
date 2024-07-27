// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.DAL.Test;

public class RolesTest
{
    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=mentor_db;User ID=postgres;Password=1234;Pooling=true;SearchPath=test;";

    private const string RoleName = "guest";
    private const string RoleNameBad = "admin";

    [Fact]
    public async Task GetAsync_PositiveTest()
    {
        var roles = new Roles(ConnectionString);
        var actual = await roles.GetAsync(1);

        var expected = new Role
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

    [Fact]
    public async Task GetAllAsync_PositiveTest()
    {
        //TODO: Implement test
        Assert.True(true);
    }

    [Fact]
    public async Task GetAllAsync_NegativeTest()
    {
        //TODO: Implement test
        Assert.False(false);
    }

    [Fact]
    public async Task InsertAndDeleteAsync_PositiveTest()
    {
        var role = new Role { RoleName = RoleName };

        var roles = new Roles(ConnectionString);
        var result = await roles.InsertAsync(role);
        Assert.True(result);

        var id = await GetIdTestRole();
        Assert.NotNull(id);

        result = await roles.DeleteAsync(id!.Value);
        Assert.True(result);
    }

    [Fact]
    public async Task InsertAsync_NegativeTest()
    {
        /*var role = new Role() { RoleName = RoleNameBad };

        var roles = new Roles(ConnectionString);
        var result = await roles.InsertAsync(role);
        Assert.False(result);*/

        //TODO: Implement test
        Assert.False(false);
    }

    private static async Task<int?> GetIdTestRole()
    {
        var roles = new Roles(ConnectionString);
        var res = await roles.GetAllAsync();
        return res?.SingleOrDefault(r => r.RoleName == RoleName)?.Id;
    }
}
