using AI_Makers_TechAssessment.Data;
using AI_Makers_TechAssessment.Models.Entities;
using AI_Makers_TechAssessment.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AI_Makers_TechAssessment.Repositories
{
    public class GenericRepository<T> : IGenericRepo<T> where T : BaseClass
    {
        protected readonly AppDbContext dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }
        public void DeleteRange(List<T> entities)
        {
            dbContext.Set<T>().RemoveRange(entities);
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await dbContext.Set<T>().AsNoTracking().ToListAsync();//IReadOnlyList//لمنع تعديل البيانات المرجعة من خارج Repository وإظهار أن النتيجة للقراءة فقط.

        }

  

        public async Task<T?> GetByIdAsync(int Id)
        {
            return await dbContext.Set<T>().FindAsync(Id);
        }

  
        public void Update( T entity)
        {
            dbContext.Set<T>().Update(entity);
           
        }



    }
}
