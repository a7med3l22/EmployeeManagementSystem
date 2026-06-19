namespace AI_Makers_TechAssessment.Models.Entities
{
    public class Department:BaseClass
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
