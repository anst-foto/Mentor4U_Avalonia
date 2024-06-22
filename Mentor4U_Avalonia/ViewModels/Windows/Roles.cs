// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System;
using System.Collections.ObjectModel;
using System.Reactive;
using DynamicData;
using Mentor4U_Avalonia.ViewModels.Controls;
using MsBox.Avalonia;
using ReactiveUI;

namespace Mentor4U_Avalonia.ViewModels.Windows;

public class Roles : ViewModelBase
{
    private const string ConnectionString = "Host=localhost;Port=5432;Database=mentor_db;User ID=postgres;Password=1234;Pooling=true;SearchPath=test;";
    public InputControlViewModel InputSearch { get; set; } = new()
    {
        Label = "Search",
        Watermark =  "Search",
        IsFloatingWatermark = false
    };

    public ObservableCollection<Models.Role> RolesCollection { get; set; }

    public ReactiveCommand<Unit, Unit> SearchCommand { get; }
    public ReactiveCommand<Unit, Unit> ViewAllCommand { get; }

    public Roles()
    {
        var canExecuteSearcCommand = this.WhenAnyValue(
            property1: p1 => p1.InputSearch.Input,
            selector: p1 => !string.IsNullOrWhiteSpace(p1));
        
        SearchCommand = ReactiveCommand.CreateFromTask(
            execute: async () =>
            {
                try
                {
                    var input = InputSearch.Input;
                    var roles = new BLL.Roles(new DAL.Roles(ConnectionString));

                    Models.Role? role;
                    if (int.TryParse(input, out var id))
                    {
                        role = await roles.GetByIdAsync(id);
                    }
                    else
                    {
                        role = await roles.GetByNameAsync(input);
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
            canExecute: canExecuteSearcCommand);

        ViewAllCommand = ReactiveCommand.CreateFromTask(
            execute: async () => 
            {
                try
                {
                    var roles = new BLL.Roles(new DAL.Roles(ConnectionString));
                    var collection = await roles.GetAllAsync();
                    RolesCollection.Clear();
                    RolesCollection.AddRange(collection);
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
    }
}