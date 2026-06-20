using AI_Makers_TechAssessment.Data;
using AI_Makers_TechAssessment.Models.Entities;
using AI_Makers_TechAssessment.Repositories.Interfaces;

namespace AI_Makers_TechAssessment.Repositories
{
    public class UnitOfWork : IUnitOfWork // تم
    {
        private readonly AppDbContext dbContext;
        private readonly Dictionary<string, object> _repo = new();

        // Cache for repositories inside the same UnitOfWork instance
        // to avoid creating multiple instances of the same repository
        // within a single request.

        // Dictionary is sufficient instead of ConcurrentDictionary
        // because UnitOfWork is registered as Scoped,
        // meaning each HTTP request has its own instance,
        // so there is no shared access across requests,
        // and thread-safety is not required here.

        public UnitOfWork(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IGenericRepo<T> GetRepo<T>() where T : BaseClass
        {
            var key = typeof(T).FullName + "_Repo";

            if (!_repo.TryGetValue(key, out var repository))
            {
                repository = new GenericRepository<T>(dbContext);
                _repo[key] = repository;
            }

            return (IGenericRepo<T>)repository;
        }

        public IEmployeeRepository GetEmployeeRepo()
        {
            var key = typeof(EmployeeRepository).Name;

            if (!_repo.TryGetValue(key, out var repository))
            {
                repository = new EmployeeRepository(dbContext);
                _repo[key] = repository;
            }

            return (IEmployeeRepository)repository;
        }

        public IGenericSpecRepo<T> GetSpecRepo<T>() where T : BaseClass
        {
            var key = typeof(T).FullName + "_Spec";

            if (!_repo.TryGetValue(key, out var repository))
            {
                repository = new GenericSpecRepo<T>(dbContext);
                _repo[key] = repository;
            }

            return (IGenericSpecRepo<T>)repository;
        }

        public async Task<int> saveChangesAsync()
        {
            return await dbContext.SaveChangesAsync(); //عدد السجلات التي تم تعديلها أو إضافتها أو حذفها في قاعدة البيانات.
        }
    }
}