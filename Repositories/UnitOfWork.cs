using AI_Makers_TechAssessment.Data;
using AI_Makers_TechAssessment.Models.Entities;
using AI_Makers_TechAssessment.Repositories.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

namespace AI_Makers_TechAssessment.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
        private readonly ConcurrentDictionary<Type, object> _repo = new();

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IGenericRepo<T> GetRepo<T>() where T : BaseClass
        {
            return (IGenericRepo<T>) _repo.GetOrAdd(typeof(T), _ => new GenericRepository<T>(dbContext));
        }
        public IEmployeeRepository GetEmployeeRepo()
        {
            return (IEmployeeRepository)_repo.GetOrAdd(typeof(EmployeeRepository), _ => new EmployeeRepository(dbContext));
        }
        public IGenericSpecRepo<T> GetSpecRepo<T>() where T : BaseClass
        {
            return (IGenericSpecRepo<T>)_repo.GetOrAdd(typeof(T), _ => new GenericSpecRepo<T>(dbContext));
        }
        public async Task<int> saveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
