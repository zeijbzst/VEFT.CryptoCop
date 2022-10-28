using System.ComponentModel.DataAnnotations;

namespace Cryptocop.Software.API.Models.InputModels
{
    public class ShoppingCartItemInputModel
    {
        [Required(ErrorMessage = "Product identifier is required.")]
        public string? ProductIdentifer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Need to provide quantity.")]
        [Range(0.01, double.MaxValue)]
        public double? Quantity { get; set; }
    }
}