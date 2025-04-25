using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Models
{
    public class FakePaymentViewModel
    {
        [Required]
        [CreditCard]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "CVV must be exactly 3 digits.")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV must be numeric and 3 digits.")]
        public string CVV { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Expiry Date")]
        public DateTime ExpiryDate { get; set; }

        [Required]
        [Display(Name = "Card Holder Name")]
        public string CardHolderName { get; set; }
    }
}
