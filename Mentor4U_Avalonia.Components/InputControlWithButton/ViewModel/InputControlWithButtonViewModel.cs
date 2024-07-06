using System.Reactive;
using ReactiveUI;

namespace Mentor4U_Avalonia.Components.ViewModel;

public class InputControlWithButtonViewModel : ViewModelBase
{
    private InputControlViewModel _inputComponent;
    public InputControlViewModel InputComponent
    {
        get => _inputComponent;
        set => this.RaiseAndSetIfChanged(ref _inputComponent, value);
    }
    
    private string _label;
    public string Label
    {
        set => InputComponent.Label = this.RaiseAndSetIfChanged(ref _label, value);
    }

    private string? _watermark;
    public string? Watermark
    {
        set => InputComponent.Watermark = this.RaiseAndSetIfChanged(ref _watermark, value);
    }
    
    private bool _isFloatingWatermark = false;
    public bool IsFloatingWatermark
    {
        set => InputComponent.IsFloatingWatermark = this.RaiseAndSetIfChanged(ref _isFloatingWatermark, value);
    }
    
    private string _button;
    public string Button
    {
        get => _button;
        set => this.RaiseAndSetIfChanged(ref _button, value);
    }

    private ReactiveCommand<Unit, Unit> _command;
    public ReactiveCommand<Unit, Unit> Command
    {
        get => _command; 
        set => this.RaiseAndSetIfChanged(ref _command, value);
    }

    public InputControlWithButtonViewModel()
    {
        InputComponent = new InputControlViewModel();
    }
}