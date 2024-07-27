using System.Text.RegularExpressions;

namespace Mentor4U_Avalonia.Models;

public class Person : IModel
{
    private int _id;
    /// <summary>
    ///     Уникальный идентификатор
    /// </summary>
    /// <exception cref="NegativeNumberException">Исключение выбрасывается, если присвоить отрицательное или нулевое значение</exception>
    public int Id
    {
        get => _id;
        set => _id = value <= 0
            ? throw new NegativeNumberException(nameof(Id))
            : value;
    }

    private string _login;
    public string Login
    {
        get => _login;
        set => _login = string.IsNullOrWhiteSpace(value)
            ? throw new EmptyStringException(nameof(Login))
            : value;
    }

    private string _password;
    public string Password
    {
        get => _password;
        set => _password = string.IsNullOrWhiteSpace(value)
            ? throw new EmptyStringException(nameof(Password))
            : value;
    }

    private string _roleName;
    public string RoleName
    {
        get => _roleName;
        set => _roleName = string.IsNullOrWhiteSpace(value)
            ? throw new EmptyStringException(nameof(RoleName))
            : value;
    }

    private string _lastName;
    public string LastName
    {
        get => _lastName;
        set => _lastName = string.IsNullOrWhiteSpace(value)
            ? throw new EmptyStringException(nameof(LastName))
            : value;
    }

    private string _firstName;
    public string FirstName
    {
        get => _firstName;
        set => _firstName = string.IsNullOrWhiteSpace(value)
            ? throw new EmptyStringException(nameof(FirstName))
            : value;
    }

    private string _patronymicName;
    public string PatronymicName
    {
        get => _patronymicName;
        set => _patronymicName = string.IsNullOrWhiteSpace(value)
            ? throw new EmptyStringException(nameof(PatronymicName))
            : value;
    }

    private DateTime _dateOfBirth;
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set => _dateOfBirth = value > DateTime.Now
            ? throw new FutureDateException(nameof(DateOfBirth))
            : value;
    }

    private string _telegram;
    public string Telegram
    {
        get => _telegram;
        set => _telegram = string.IsNullOrWhiteSpace(value)
            ? throw new EmptyStringException(nameof(Telegram))
            : value;
    }

    private string _email;
    public string Email
    {
        get => _email;
        set => _email = !Regex.IsMatch(value, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+.+.[A-Za-z]{2,4}")
            ? throw new InvalidEmailException(nameof(Email))
            : value;
    }

    private string _photoPath;
    public string PhotoPath
    {
        get => _photoPath;
        set => _photoPath = string.IsNullOrWhiteSpace(value)
            ? throw new EmptyStringException(nameof(PhotoPath))
            : value;
    }
}
