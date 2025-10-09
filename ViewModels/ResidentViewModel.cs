using ITI_SC_Project.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ITI_SC_Project.ViewModels
{
    public class ResidentViewModel
    {
        [ScaffoldColumn(false)]
        public int InternalId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Identity number must be between 5 and 20 characters.")]
        [Display(Name = "Identity / Passport Number")]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(150)]
        public string Email { get; set; } = null!;

        [PhoneNumber]
        [StringLength(20)]
        public string Phone { get; set; } = null!;

        [Display(Name = "Active")]
        public bool IsActive { get; set; } = true;
    }
}
