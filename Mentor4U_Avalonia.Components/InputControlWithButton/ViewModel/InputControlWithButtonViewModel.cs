using System.Reactive;
using ReactiveUI;

namespace Mentor4U_Avalonia.Components.ViewModel;

public class InputControlWithButtonViewModel : ViewModelBase
{
    private string _button;

    private ReactiveCommand<Unit, Unit> _command;
    private InputControlViewModel _inputComponent;

    private bool _isFloatingWatermark;

    private string _label;

    private string? _watermark;

    public InputControlWithButtonViewModel()
    {
        InputComponent = new InputControlViewModel();
    }

    public InputControlViewModel InputComponent
    {
        get => _inputComponent;
        set => this.RaiseAndSetIfChanged(ref _inputComponent, value);
    }

    public string Label
    {
        set => InputComponent.Label = this.RaiseAndSetIfChanged(ref _label, value);
    }

    public string? Watermark
    {
        set => InputComponent.Watermark = this.RaiseAndSetIfChanged(ref _watermark, value);
    }

    public bool IsFloatingWatermark
    {
        set => InputComponent.IsFloatingWatermark = this.RaiseAndSetIfChanged(ref _isFloatingWatermark, value);
    }

    public string Button
    {
        get => _button;
        set => this.RaiseAndSetIfChanged(ref _button, value);
    }

    public ReactiveCommand<Unit, Unit> Command
    {
        get => _command;
        set => this.RaiseAndSetIfChanged(ref _command, value);
    }
}
