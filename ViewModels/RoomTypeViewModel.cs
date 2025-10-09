using System.ComponentModel.DataAnnotations;

namespace ITI_SC_Project.ViewModels
{
    public class RoomTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Type name cannot exceed 50 characters.")]
        [Display(Name = "Room Type")]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0, 100000, ErrorMessage = "Base price must be between 0 and 100,000.")]
        [Display(Name = "Base Price (per night)")]
        [DataType(DataType.Currency)]
        public decimal BasePrice { get; set; }
    }
}
