// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.DAL.Test;

public class DbServiceTest
{
    private const string ConnectionStringGood = "Host=localhost;Port=5432;Database=mentor_db;User ID=postgres;Password=1234;Pooling=true;SearchPath=test;";
    private const string ConnectionStringEmpty = "";
    
    [Fact]
    public async Task ConnectAsync_PositiveTest_ConnectionStringGood()
    {
        var result = await Record.ExceptionAsync(async () => { await DbService<Role>.ConnectAsync(ConnectionStringGood);});
        Assert.Null(result);
    }

    [Fact]
    public async Task ConnectAsync_NegativeTest_ConnectionStringEmpty()
    {
        await Assert.ThrowsAsync<EmptyStringException>(async () => { await DbService<Role>.ConnectAsync(ConnectionStringEmpty); });
    }
}