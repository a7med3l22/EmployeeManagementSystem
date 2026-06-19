namespace AI_Makers_TechAssessment.Models.Entities
{
    public class MobileNumber: BaseClass
    {
        public string Number { get; set; } = null!;
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
