using ITI_SC_Project.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ITI_SC_Project.ViewModels
{
    public class SelectDatesViewModel
    {
        public int ResidentId { get; set; }

        public string ResidentName { get; set; } = null!;

        public string ResidentCode { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check-In Date")]
        public DateOnly CheckIn { get; set; } = DateOnly.FromDateTime(DateTime.Today);

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check-Out Date")]
        [DateGreaterThan(nameof(CheckIn), ErrorMessage = "Check-out must be after check-in.")]
        public DateOnly CheckOut { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(1));
    }
}
