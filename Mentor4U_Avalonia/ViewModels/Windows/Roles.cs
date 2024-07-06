// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using DynamicData;
using Mentor4U_Avalonia.Components;
using Mentor4U_Avalonia.Components.ViewModel;
using Mentor4U_Avalonia.Models;
using MsBox.Avalonia;
using ReactiveUI;

namespace Mentor4U_Avalonia.ViewModels.Windows;

public class Roles : ViewModelBase
{
    private const string ConnectionString = "Host=localhost;Port=5432;Database=mentor_db;User ID=postgres;Password=1234;Pooling=true;SearchPath=test;";

    private readonly BLL.Roles _roles;
    
    public InputControlWithButtonViewModel InputSearch { get; set; } = new()
    {
        Label = "Search",
        Watermark =  "Search",
        IsFloatingWatermark = false,
        Button = "Search"
    };
    public InputControlWithButtonViewModel InputNew { get; set; } = new()
    {
        Label = "New",
        Watermark =  "Add new role",
        IsFloatingWatermark = false,
        Button = "Save"
    };

    public ObservableCollection<Models.Role> RolesCollection { get; set; } = [];

    private Models.Role? _selectedRole;
    public Models.Role? SelectedRole
    {
        get => _selectedRole;
        set => this.RaiseAndSetIfChanged(ref _selectedRole, value);
    }
    
    public ReactiveCommand<Unit, Unit> ViewAllCommand { get; }
    public ReactiveCommand<Role, Unit> DeleteCommand { get; }

    public Roles()
    {
        _roles = new BLL.Roles(new DAL.Roles(ConnectionString));
        
        var canExecuteSearchCommand = this.WhenAnyValue(
            property1: p1 => p1.InputSearch.InputComponent.Input,
            selector: p1 => !string.IsNullOrWhiteSpace(p1));
        var canExecuteNewCommand = this.WhenAnyValue(
            property1: p1 => p1.InputNew.InputComponent.Input,
            selector: p1 => !string.IsNullOrWhiteSpace(p1));
        
        InputSearch.Command = ReactiveCommand.CreateFromTask(
            execute: async () =>
            {
                try
                {
                    var input = InputSearch.InputComponent.Input;

                    Models.Role? role;
                    if (int.TryParse(input, out var id))
                    {
                        role = await _roles.GetByIdAsync(id);
                    }
                    else
                    {
                        role = await _roles.GetByNameAsync(input);
                    }
                    
                    await ShowInfo($"{role.Id} - {role.RoleName}");
                }
                catch (Exception ex)
                {
                    await ShowError(ex.Message);
                }
            },
            canExecute: canExecuteSearchCommand);
        InputNew.Command = ReactiveCommand.CreateFromTask(
            execute: async () =>
            {
                try
                {
                    var input = InputNew.InputComponent.Input;

                    var role = new Models.Role()
                    {
                        RoleName  = input!
                    };
                    var result = await _roles.CreateAsync(role);
                    if (result is not null)
                    {
                        await ShowInfo($"{role.Id} - {role.RoleName}");
                        await UpdateRolesCollectionAsync();
                    }
                    else
                    {
                        await ShowInfo($"Роль с именем {role.RoleName} не создалась");
                    }
                }
                catch (Exception ex)
                {
                    await ShowError(ex.Message);
                }
            },
            canExecute: canExecuteNewCommand);

        ViewAllCommand = ReactiveCommand.CreateFromTask(
            execute: async () => 
            {
                try
                {
                    await UpdateRolesCollectionAsync();
                }
                catch (Exception ex)
                {
                    await ShowError(ex.Message);
                }
            });
        
        DeleteCommand = ReactiveCommand.CreateFromTask<Role, Unit>(
            execute: async (role) =>
            {
                try
                {
                    await _roles.DeleteAsync(role);
                    await UpdateRolesCollectionAsync();
                }
                catch (Exception ex)
                {
                    await ShowError(ex.Message);
                }

                return default;
            });
    }

    private async Task UpdateRolesCollectionAsync()
    {
        var collection = await _roles.GetAllAsync();
        RolesCollection.Clear();
        RolesCollection.AddRange(collection);
    }

    private static async Task ShowError(string message)
    {
        await MessageBox.ShowError(
            title: $"{App.Current.Resources["AppTitle"]} - Roles",
            text: message);
    }

    private static async Task ShowInfo(string message)
    {
        await MessageBox.ShowInfo(
            title: $"{App.Current.Resources["AppTitle"]} - Roles",
            text: message);
    }
}