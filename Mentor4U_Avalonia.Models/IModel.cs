// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

namespace Mentor4U_Avalonia.Models;

/// <summary>
///     Интерфейс модели данных
/// </summary>
public interface IModel
{
    /// <summary>
    ///     Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }
}
