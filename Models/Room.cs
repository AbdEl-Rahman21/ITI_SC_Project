namespace ITI_SC_Project.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public decimal Price { get; set; }

        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
