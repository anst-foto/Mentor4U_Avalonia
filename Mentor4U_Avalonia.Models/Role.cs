// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

namespace Mentor4U_Avalonia.Models;

public record class Role : IModel
{
    private int _id;
    public int Id
    {
        get => _id;
        set => _id = value <= 0 
            ? throw new NegativeNumberException(nameof(Id)) 
            : value;
    }
    
    private string _roleName;
    public string RoleName
    {
        get => _roleName;
        set => _roleName = string.IsNullOrWhiteSpace(value) 
            ? throw new EmptyStringException(nameof(RoleName)) 
            : value;
    }
}