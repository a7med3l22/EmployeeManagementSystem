using AI_Makers_TechAssessment.Models.Entities;
using System.Linq.Expressions;

namespace AI_Makers_TechAssessment.Repositories.Interfaces
{
    public interface IGenericSpecRepo<T>:IGenericRepo<T> where T : BaseClass
    {
        Task<IReadOnlyList<T>> SearchAsync(Expression<Func<T, bool>>? Predicate, params Expression<Func<T, object>>[] funcsSpec);
    
    
		Task<T?> GetByIdSpecAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] funcs);
        Task<IReadOnlyList<T>> GetAllSpecAsync(params Expression<Func<T, object>>[] func);


    }
}
