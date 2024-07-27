// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.DAL;

public static class DbHelper
{
    public static readonly Dictionary<Type, string> TableNames = new()
    {
        { typeof(Role), "table_roles" }
    };

    public static readonly Dictionary<string, string> RoleTablesColumnNames = new()
    {
        { nameof(Role.Id), "id" },
        { nameof(Role.RoleName), "role_name" }
    };
}
