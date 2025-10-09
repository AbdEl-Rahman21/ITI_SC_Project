using System.ComponentModel.DataAnnotations;

namespace ITI_SC_Project.ViewModels
{
    public class BoardingTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Boarding type name cannot exceed 50 characters.")]
        public string Name { get; set; } = null!;

        [Range(0, 10000, ErrorMessage = "Modifier must be between 0 and 10,000.")]
        [Display(Name = "Price Modifier (%)")]
        public decimal PriceModifier { get; set; }
    }
}
