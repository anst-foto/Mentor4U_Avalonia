// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using ReactiveUI;

namespace Mentor4U_Avalonia.ViewModels.Controls;

public class InputControlViewModel : ViewModelBase
{
    private string _label;
    public string Label
    {
        get => _label;
        set => this.RaiseAndSetIfChanged(ref _label, value);
    }

    private string? _input;
    public string? Input
    {
        get => _input;
        set => this.RaiseAndSetIfChanged(ref _input, value);
    }
    
    private string? _watermark;
    public string? Watermark
    {
        get => _watermark;
        set => this.RaiseAndSetIfChanged(ref _watermark, value);
    }
    
    private bool _isFloatingWatermark = false;
    public bool IsFloatingWatermark
    {
        get => _isFloatingWatermark;
        set => this.RaiseAndSetIfChanged(ref _isFloatingWatermark, value);
    }
}