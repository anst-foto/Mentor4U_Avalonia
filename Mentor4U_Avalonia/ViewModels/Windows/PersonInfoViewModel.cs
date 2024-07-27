using System;
using System.Reactive;
using Mentor4U_Avalonia.BLL;
using Mentor4U_Avalonia.Components.ViewModel;
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

    //public InputControlViewModel InputPassword { get; set; } = new();
    //public InputControlViewModel InputRole { get; set; } = new();
    //public InputControlViewModel InputLastName { get; set; } = new();
    //public InputControlViewModel InputFirstName { get; set; } = new();
    //public InputControlViewModel InputPatronymicName { get; set; } = new();
    //public InputControlViewModel InputTelegram { get; set; } = new();
    //public InputControlViewModel InputEmail { get; set; } = new();

    public DateTime DateOfBirth { get; set; } = DateTime.Now;
    public string PhotoPath { get; set; } = "";

    private const string ConnectionString =
        "Host=localhost;Port=5432;Database=mentor_db;User ID=postgres;Password=1234;Pooling=true;SearchPath=test;";

    public ReactiveCommand<Unit, Unit> GetCommand { get; }

    public PersonInfoViewModel()
    {
        InputLogin = new InputControlViewModel()
        {
            Label = "",
            Input = ""
        };

        GetCommand = ReactiveCommand.CreateFromTask(async () =>
        {
             var persons = new BLL.Persons(new DAL.Persons(ConnectionString));
                    var person = await persons.GetByLoginAsync("user"); //TODO: Сделать проверку на null

                    InputLogin.Label = "Логин";
                    InputLogin.Input = person.Login;
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
