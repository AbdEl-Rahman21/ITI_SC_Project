namespace ITI_SC_Project.Models
{
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public decimal Salary { get; set; }
        public string JobTitle { get; set; } = null!;
    }
}
