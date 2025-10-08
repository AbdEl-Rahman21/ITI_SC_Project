namespace ITI_SC_Project.Models
{
    public class Resident
    {
        public int Id { get; set; }
        public string ResidentId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
