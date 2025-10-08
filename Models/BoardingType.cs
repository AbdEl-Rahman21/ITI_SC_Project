namespace ITI_SC_Project.Models
{
    public class BoardingType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal PriceModifier { get; set; }

        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
