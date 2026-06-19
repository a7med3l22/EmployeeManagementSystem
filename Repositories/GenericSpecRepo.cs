using AI_Makers_TechAssessment.Data;
using AI_Makers_TechAssessment.Models.Entities;
using AI_Makers_TechAssessment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AI_Makers_TechAssessment.Repositories
{
    public class GenericSpecRepo<T> : GenericRepository<T>, IGenericSpecRepo<T> where T : BaseClass
    {
        public GenericSpecRepo(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<T>> SearchAsync(Expression<Func<T, bool>>? Predicate, params Expression<Func<T, object>>[] funcsSpec)
        {
            IQueryable<T> query = dbContext.Set<T>().AsNoTracking();

            if (Predicate != null)
            {
                query = query.Where(Predicate);
            }

            query = funcsSpec.Aggregate(query, (current, func) => current.Include(func));


            return await query.ToListAsync();
        }


        public async Task<T?> GetByIdSpecAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] funcs)
        {
            IQueryable<T> query = dbContext.Set<T>();
            query = funcs.Aggregate(query, (value, func) => value.Include(func));

           
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IReadOnlyList<T>> GetAllSpecAsync(params Expression<Func<T, object>>[] funcs)
        {
            IQueryable<T> query = dbContext.Set<T>().AsNoTracking();
            query = funcs.Aggregate(query, (value, func) => value.Include(func));
            return await query.AsNoTracking().ToListAsync();

        }
    }
}
