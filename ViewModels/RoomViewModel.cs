using System.ComponentModel.DataAnnotations;

namespace ITI_SC_Project.ViewModels
{
    public class RoomViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Room Type")]
        public int RoomTypeId { get; set; }

        public string? RoomTypeName { get; set; }
    }
}
