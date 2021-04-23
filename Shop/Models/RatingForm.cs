using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class RatingForm
    {

        [Required(ErrorMessage = "The {0} field is required.")]
        public long ProductId { get; set; }

        [MaxLength(60, ErrorMessage = "Tối đa 60 ký tự")]
        [Required(ErrorMessage = "The {0} field is required.")]
        public string Name { get; set; }
        
        [MaxLength(60, ErrorMessage = "Tối đa 60 ký tự")]
        [Required(ErrorMessage = "The {0} field is required.")]
        public string Email { get; set; }
        [MaxLength(1000, ErrorMessage = "Tối đa 1000 ký tự")]
        [Required(ErrorMessage = "The {0} field is required.")]
        public string Content { get; set; }

        [Required(ErrorMessage = "The {0} field is required.")]
        public decimal Scores { get; set; }

    }
}
