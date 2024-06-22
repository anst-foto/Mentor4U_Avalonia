// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Reactive;
using System.Threading.Tasks;
using Mentor4U_Avalonia.ViewModels.Controls;
using MsBox.Avalonia;
using ReactiveUI;

namespace Mentor4U_Avalonia.ViewModels.Windows;

public class AuthWindowViewModel : ViewModelBase
{
    /*private string? _login;
    public string? Login
    {
        get => _login;
        set
        {
            this.RaiseAndSetIfChanged(ref _login, value);
            AuthData.Login = _login;
        }
    }

    private string? _password;
    public string? Password
    {
        get => _password;
        set
        {
            this.RaiseAndSetIfChanged(ref _password, value);
            AuthData.Password = _password;
        }
    }*/
    
    public InputControlViewModel InputLogin { get; set; } = new()
    {
        Label = "Login",
        Watermark = "Введите логин",
        IsFloatingWatermark = true
    };

    public InputControlViewModel InputPassword { get; set; } = new()
    {
        Label = "Password",
        Watermark = "Введите пароль",
        IsFloatingWatermark = false
    };

    /*private AuthData _authData;

    public AuthData AuthData
    {
        get => _authData;
        set => this.RaiseAndSetIfChanged(ref _authData, value);
    }*/

    public ReactiveCommand<Unit, Task> AuthCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearCommand { get; }

    public AuthWindowViewModel()
    {
        /*_authData = new AuthData();*/

        /*var canExecuteAuthCommand = this.WhenAnyValue(
            property1: p1 => p1.Login,
            property2: p2 => p2.Password,
            selector: (p1, p2) => !string.IsNullOrWhiteSpace(p1) && !string.IsNullOrWhiteSpace(p2));
        AuthCommand = ReactiveCommand.Create<AuthData>(
            execute: async authData =>
            {
                await MessageBoxManager
                    .GetMessageBoxStandard(
                        title: $"{App.Current.Resources["AppTitle"]} - Auth",
                        text: $"{authData.Login} - {authData.Password}")
                    .ShowAsync();
            },
            canExecute: canExecuteAuthCommand);

        var canExecuteClearCommand = this.WhenAnyValue(
            property1: p1 => p1.Login,
            property2: p2 => p2.Password,
            selector: (p1, p2) => !string.IsNullOrWhiteSpace(p1) || !string.IsNullOrWhiteSpace(p2));
        ClearCommand = ReactiveCommand.Create(
            execute: () =>
            {
                Login = null;
                Password = null;
            },
            canExecute: canExecuteClearCommand);*/
        
        var canExecuteAuthCommand = this.WhenAnyValue(
            property1: p1 => p1.InputLogin.Input,
            property2: p2 => p2.InputPassword.Input,
            selector: (p1, p2) => !string.IsNullOrWhiteSpace(p1) && !string.IsNullOrWhiteSpace(p2));
        
        AuthCommand = ReactiveCommand.Create(
            execute: async () =>
            {
                await MessageBoxManager
                    .GetMessageBoxStandard(
                        title: $"{App.Current.Resources["AppTitle"]} - Auth",
                        text: $"{InputLogin.Input} - {InputPassword.Input}")
                    .ShowAsync();
            },
            canExecute: canExecuteAuthCommand);
    }
}