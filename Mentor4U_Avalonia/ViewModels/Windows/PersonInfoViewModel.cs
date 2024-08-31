using System;
using System.Reactive;
using Avalonia.Media.Imaging;
using Mentor4U_Avalonia.Components.ViewModel;
using Mentor4U_Avalonia.Lib;
using Microsoft.Extensions.Logging;
using ReactiveUI;

namespace Mentor4U_Avalonia.ViewModels.Windows;

public class PersonInfoViewModel : ViewModelBase
{
    private InputControlViewModel _inputLogin;
    public InputControlViewModel InputLogin
    {
        get => _inputLogin;
        set => this.RaiseAndSetIfChanged(ref _inputLogin, value);
    }

    private Bitmap _photo;
    public Bitmap Photo
    {
        get => _photo;
        set => this.RaiseAndSetIfChanged(ref _photo, value);
    }

    //public InputControlViewModel InputPassword { get; set; } = new();
    //public InputControlViewModel InputRole { get; set; } = new();
    //public InputControlViewModel InputLastName { get; set; } = new();
    //public InputControlViewModel InputFirstName { get; set; } = new();
    //public InputControlViewModel InputPatronymicName { get; set; } = new();
    //public InputControlViewModel InputTelegram { get; set; } = new();
    //public InputControlViewModel InputEmail { get; set; } = new();

    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=mentor_db;User ID=postgres;Password=1234;Pooling=true;SearchPath=test;";

    public ReactiveCommand<Unit, Unit> GetCommand { get; }

    public PersonInfoViewModel() : base()
    {
        Logger = loggerFactory.CreateLogger<PersonInfoViewModel>();

        _inputLogin = new InputControlViewModel();

        GetCommand = ReactiveCommand.CreateFromTask(async () =>
        {
             var persons = new BLL.Persons(new DAL.Persons(ConnectionString));
                    var person = await persons.GetByLoginAsync("user"); //TODO: Сделать проверку на null
                    Logger.LogInformation("Получение данных");

                    InputLogin.Label = "Логин";
                    InputLogin.Input = person.Login;

                    Photo = ImageHelper.LoadFromDisk(new Uri(person.PhotoPath));

                    /*InputPassword = new InputControlViewModel()
                    {
                        Label = "Пароль",
                        Input = person.Password
                    };
                    InputRole = new InputControlViewModel()
                    {
                        Label = "Роль",
                        Input = person.RoleName
                    };
                    InputLastName = new InputControlViewModel()
                    {
                        Label = "Фамилия",
                        Input = person.LastName
                    };
                    InputFirstName = new InputControlViewModel()
                    {
                        Label = "Имя",
                        Input = person.FirstName
                    };
                    InputPatronymicName = new InputControlViewModel()
                    {
                        Label = "Отчество",
                        Input = person.PatronymicName
                    };
                    InputTelegram = new InputControlViewModel()
                    {
                        Label = "telegram",
                        Input = person.Telegram
                    };
                    InputEmail = new InputControlViewModel()
                    {
                        Label = "email",
                        Input = person.Email
                    };

                    DateOfBirth = person.DateOfBirth;
                    PhotoPath = person.PhotoPath;*/
        });
    }
}
