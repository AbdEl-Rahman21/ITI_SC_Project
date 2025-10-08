namespace ITI_SC_Project.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; } = null!;

        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
