// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using ReactiveUI;

namespace Mentor4U_Avalonia.Components.ViewModel;

public class InputControlViewModel : ViewModelBase
{
    private string? _input;

    private bool _isFloatingWatermark;
    private string _label;

    private string? _watermark;

    public string Label
    {
        get => _label;
        set => this.RaiseAndSetIfChanged(ref _label, value);
    }

    public string? Input
    {
        get => _input;
        set => this.RaiseAndSetIfChanged(ref _input, value);
    }

    public string? Watermark
    {
        get => _watermark;
        set => this.RaiseAndSetIfChanged(ref _watermark, value);
    }

    public bool IsFloatingWatermark
    {
        get => _isFloatingWatermark;
        set => this.RaiseAndSetIfChanged(ref _isFloatingWatermark, value);
    }
}
