using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.DAL;

public static class DbHelper
{
    public static readonly Dictionary<Type, string> TableNames = new Dictionary<Type, string>()
    {
        {typeof(Role), "table_roles"}
    };
}