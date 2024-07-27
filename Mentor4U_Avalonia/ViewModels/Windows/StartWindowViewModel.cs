// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using AuthWindow = Mentor4U_Avalonia.Views.Windows.AuthWindow;

namespace Mentor4U_Avalonia.ViewModels.Windows;

public class StartWindowViewModel : ViewModelBase
{
    public void CloseWindow()
    {
        (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow.Close();
    }

    public void Auth()
    {
        var window = new AuthWindow();
        window.Show();

        CloseWindow();
    }
}
