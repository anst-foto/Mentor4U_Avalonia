// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Mentor4U_Avalonia.ViewModels.Windows;
using Mentor4U_Avalonia.Views.Windows;
using Roles = Mentor4U_Avalonia.Views.Windows.Roles;

namespace Mentor4U_Avalonia;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new PersonInfoWindow()
            {
                DataContext = new PersonInfoViewModel()
            };

        base.OnFrameworkInitializationCompleted();
    }
}
