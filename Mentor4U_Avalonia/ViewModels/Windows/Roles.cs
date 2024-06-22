// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using DynamicData;
using Mentor4U_Avalonia.Models;
using Mentor4U_Avalonia.ViewModels.Controls;
using MsBox.Avalonia;
using ReactiveUI;

namespace Mentor4U_Avalonia.ViewModels.Windows;

public class Roles : ViewModelBase
{
    private const string ConnectionString = "Host=localhost;Port=5432;Database=mentor_db;User ID=postgres;Password=1234;Pooling=true;SearchPath=test;";

    private readonly BLL.Roles _roles;
    
    public InputControlViewModel InputSearch { get; set; } = new()
    {
        Label = "Search",
        Watermark =  "Search",
        IsFloatingWatermark = false
    };
    public InputControlViewModel InputNew { get; set; } = new()
    {
        Label = "New",
        Watermark =  "Add new role",
        IsFloatingWatermark = false
    };

    public ObservableCollection<Models.Role> RolesCollection { get; set; } = [];

    private Models.Role? _selectedRole;
    public Models.Role? SelectedRole
    {
        get => _selectedRole;
        set => this.RaiseAndSetIfChanged(ref _selectedRole, value);
    }

    public ReactiveCommand<Unit, Unit> SearchCommand { get; }
    public ReactiveCommand<Unit, Unit> ViewAllCommand { get; }
    public ReactiveCommand<Unit, Unit> NewCommand { get; }
    public ReactiveCommand<Role, Unit> DeleteCommand { get; }

    public Roles()
    {
        _roles = new BLL.Roles(new DAL.Roles(ConnectionString));
        
        var canExecuteSearchCommand = this.WhenAnyValue(
            property1: p1 => p1.InputSearch.Input,
            selector: p1 => !string.IsNullOrWhiteSpace(p1));
        var canExecuteNewCommand = this.WhenAnyValue(
            property1: p1 => p1.InputNew.Input,
            selector: p1 => !string.IsNullOrWhiteSpace(p1));
        
        SearchCommand = ReactiveCommand.CreateFromTask(
            execute: async () =>
            {
                try
                {
                    var input = InputSearch.Input;

                    Models.Role? role;
                    if (int.TryParse(input, out var id))
                    {
                        role = await _roles.GetByIdAsync(id);
                    }
                    else
                    {
                        role = await _roles.GetByNameAsync(input);
                    }

                    await MessageBoxManager
                        .GetMessageBoxStandard(
                            title: $"{App.Current.Resources["AppTitle"]} - Roles",
                            text: $"{role.Id} - {role.RoleName}")
                        .ShowAsync();
                }
                catch (Exception ex)
                {
                    await MessageBoxManager
                        .GetMessageBoxStandard(
                            title: $"{App.Current.Resources["AppTitle"]} - Roles",
                            text: $"{ex.Message}")
                        .ShowAsync();
                }
            },
            canExecute: canExecuteSearchCommand);

        ViewAllCommand = ReactiveCommand.CreateFromTask(
            execute: async () => 
            {
                try
                {
                    await UpdateRolesCollectionAsync();
                }
                catch (Exception ex)
                {
                    await MessageBoxManager
                        .GetMessageBoxStandard(
                            title: $"{App.Current.Resources["AppTitle"]} - Roles",
                            text: $"{ex.Message}")
                        .ShowAsync();
                }
            });
        
        NewCommand = ReactiveCommand.CreateFromTask(
            execute: async () =>
            {
                try
                {
                    var input = InputNew.Input;

                    var role = new Models.Role()
                    {
                        RoleName  = input!
                    };
                    var result = await _roles.CreateAsync(role);
                    if (result is not null)
                    {
                        await MessageBoxManager
                            .GetMessageBoxStandard(
                                title: $"{App.Current.Resources["AppTitle"]} - Roles",
                                text: $"{result.Id} - {result.RoleName}")
                            .ShowAsync();
                        
                        await UpdateRolesCollectionAsync();
                    }
                    else
                    {
                        await MessageBoxManager
                            .GetMessageBoxStandard(
                                title: $"{App.Current.Resources["AppTitle"]} - Roles",
                                text: $"Роль с именем {role.RoleName} не создалась")
                            .ShowAsync();
                    }
                }
                catch (Exception ex)
                {
                    await MessageBoxManager
                        .GetMessageBoxStandard(
                            title: $"{App.Current.Resources["AppTitle"]} - Roles",
                            text: $"{ex.Message}")
                        .ShowAsync();
                }
            },
            canExecute: canExecuteNewCommand);

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
                    await MessageBoxManager
                        .GetMessageBoxStandard(
                            title: $"{App.Current.Resources["AppTitle"]} - Roles",
                            text: $"{ex.Message}")
                        .ShowAsync();
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
}