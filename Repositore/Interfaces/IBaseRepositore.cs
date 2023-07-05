using EL.BlackList.API.Models;

namespace EL.BlackList.API.Repositore.Interfaces
{
    public interface IBaseRepositore<T>
    {
        Task<bool> Delete(int id);
        Task<IEnumerable<T>> Select();
        Task<T?> GetById(int id);
        Task<int> Save(T intite);
    }
}
