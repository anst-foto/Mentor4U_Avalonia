// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Mentor4U_Avalonia.Components.ViewModel;
using MsBox.Avalonia;
using ReactiveUI;

namespace Mentor4U_Avalonia.ViewModels.Windows;

public class AuthWindowViewModel : ViewModelBase
{
    public AuthWindowViewModel()
    {
        var canExecuteAuthCommand = this.WhenAnyValue(
            p1 => p1.InputLogin.Input,
            p2 => p2.InputPassword.Input,
            (p1, p2) => !string.IsNullOrWhiteSpace(p1) && !string.IsNullOrWhiteSpace(p2));
        var canExecuteClearCommand = this.WhenAnyValue(
            p1 => p1.InputLogin.Input,
            p2 => p2.InputPassword.Input,
            (p1, p2) => !string.IsNullOrWhiteSpace(p1) || !string.IsNullOrWhiteSpace(p2)
        );

        AuthCommand = ReactiveCommand.Create(
            async () =>
            {
                await MessageBoxManager
                    .GetMessageBoxStandard(
                        $"{Application.Current.Resources["AppTitle"]} - Auth",
                        $"{InputLogin.Input} - {InputPassword.Input}")
                    .ShowAsync();
            },
            canExecuteAuthCommand);
        ClearCommand = ReactiveCommand.Create(
            () =>
            {
                InputLogin.Input = string.Empty;
                InputPassword.Input = string.Empty;
            },
            canExecuteClearCommand);
    }

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

    public ReactiveCommand<Unit, Task> AuthCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearCommand { get; }
}
