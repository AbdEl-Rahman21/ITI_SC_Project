namespace ITI_SC_Project.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public ICollection<Room> Rooms { get; set; } = [];
    }
}
