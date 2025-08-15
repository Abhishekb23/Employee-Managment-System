namespace Managment_back.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation
        public ICollection<Employee> Employees { get; set; }
    }
}
