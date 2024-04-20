using ReactiveUI;

namespace Mentor4U_Avalonia;

public class AuthData : ReactiveObject
{
    private string? _login;
    public string? Login
    {
        get => _login;
        set => this.RaiseAndSetIfChanged(ref _login, value);
    }
    
    private string? _password;
    public string? Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
}