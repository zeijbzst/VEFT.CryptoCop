using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Cryptocop.Software.API.Models.InputModels
{
    public class ShoppingCartItemInputModel
    {
        public string? ProductIdentifier { get; set; } = string.Empty;

        [Required(ErrorMessage = "Need to provide quantity.")]
        [Range(0.01, double.MaxValue)]
        public float Quantity { get; set; }
    }
}