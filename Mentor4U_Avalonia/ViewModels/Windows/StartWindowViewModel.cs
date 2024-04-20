using Avalonia.Controls.ApplicationLifetimes;
using Mentor4U_Avalonia.Views;

namespace Mentor4U_Avalonia.ViewModels.Windows;

public class StartWindowViewModel : ViewModelBase
{
    public string IsSpecial { get; set; } = "open";
    
    public void CloseWindow()
    {
        (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow.Close();
    }

    public void Auth()
    {
        var window = new AuthWindow();
        window.Show();
        
        CloseWindow();
    }
}