using System.ComponentModel.DataAnnotations;

namespace Cryptocop.Software.API.Models.InputModels
{
    public class PaymentCardInputModel
    {
        [Required(ErrorMessage = "Cardholder name is required.")]
        [MinLength(3, ErrorMessage = "Cardholder name needs to be atleast 3 characters.")]
        public string CardholderName { get; set; }

        [Required(ErrorMessage = "Credit card number is required.")]
        [CreditCard(ErrorMessage = "Must be a valid credit card number.")]
        public string CardNumber { get; set; }

        [Range(1, 12, ErrorMessage = "Needs to be between 1 and 12")]
        public int Month { get; set; }

        [Range(0, 99, ErrorMessage = "Needs to be between 0 and 99")]
        public int Year { get; set; }
    }
}