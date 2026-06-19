using AI_Makers_TechAssessment.Models.Entities;

namespace AI_Makers_TechAssessment.Repositories.Interfaces
{
    public interface IGenericRepo<T> where T : BaseClass
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int Id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(List<T> entities);

    }
}
