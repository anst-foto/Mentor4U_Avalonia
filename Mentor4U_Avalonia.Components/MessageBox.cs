using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace Mentor4U_Avalonia.Components;

public static class MessageBox
{
    public static async Task ShowInfo(string title, string text)
    {
        await MessageBoxManager
            .GetMessageBoxStandard(title, text, ButtonEnum.Ok, Icon.Info)
            .ShowAsync();
    }

    public static async Task ShowError(string title, string text)
    {
        await MessageBoxManager
            .GetMessageBoxStandard(title, text, ButtonEnum.Ok, Icon.Error)
            .ShowAsync();
    }
}
