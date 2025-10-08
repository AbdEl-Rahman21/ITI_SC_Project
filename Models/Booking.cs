namespace ITI_SC_Project.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateOnly CheckInDate { get; set; }
        public DateOnly CheckOutDate { get; set; }
        public decimal TotalCost { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; } = null!;

        public int ResidentId { get; set; }
        public Resident Resident { get; set; } = null!;

        public int BoardingTypeId { get; set; }
        public BoardingType BoardingType { get; set; } = null!;
    }
}
