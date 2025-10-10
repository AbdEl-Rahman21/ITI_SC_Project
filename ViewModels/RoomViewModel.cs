using System.ComponentModel.DataAnnotations;

namespace ITI_SC_Project.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "Type name cannot exceed 10 characters.")]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; } = null!;

        [Required]
        [Display(Name = "Room Type")]
        public int RoomTypeId { get; set; }

        public string? RoomTypeName { get; set; }
    }
}
