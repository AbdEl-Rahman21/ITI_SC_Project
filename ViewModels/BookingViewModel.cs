using ITI_SC_Project.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ITI_SC_Project.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check-In Date")]
        public DateOnly CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check-Out Date")]
        [DateGreaterThan(nameof(CheckInDate), ErrorMessage = "Check-out must be after check-in.")]
        public DateOnly CheckOutDate { get; set; }

        public decimal TotalCost { get; set; }

        [Required]
        [Display(Name = "Room")]
        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Resident")]
        public int ResidentId { get; set; }

        [Required]
        [Display(Name = "Boarding Type")]
        public int BoardingTypeId { get; set; }

        public string? RoomNumber {  get; set; }
        public string? RoomTypeName { get; set; }
        public string? ResidentCode { get; set; }
        public string? ResidentName { get; set; }
        public string? BoardingTypeName { get; set; }
    }
}
