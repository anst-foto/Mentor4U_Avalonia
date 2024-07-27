// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.DAL;

public interface ICrud<TEntity> where TEntity : IModel
{
    public Task<bool> InsertAsync(TEntity entity);
    public Task<bool> UpdateAsync(TEntity entity);
    public Task<bool> DeleteAsync(int id);
    public Task<TEntity?> GetAsync(int id);
    public Task<IEnumerable<TEntity>?> GetAllAsync();
}
