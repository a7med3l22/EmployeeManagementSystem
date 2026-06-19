using AI_Makers_TechAssessment.Models.Entities;

namespace AI_Makers_TechAssessment.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepo<T> GetRepo<T>() where T : BaseClass;
        IGenericSpecRepo<T> GetSpecRepo<T>() where T : BaseClass;

        IEmployeeRepository GetEmployeeRepo();
        Task<int> saveChangesAsync();
    }
}
