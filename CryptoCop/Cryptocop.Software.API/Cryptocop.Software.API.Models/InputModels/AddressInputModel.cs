using System.ComponentModel.DataAnnotations;

namespace Cryptocop.Software.API.Models.InputModels
{
    public class AddressInputModel
    {
        [Required(ErrorMessage = "Street name is required.")]
        public string StreetName { get; set; }

        [Required(ErrorMessage = "House number is required.")]
        public string HouseNumber { get; set; }

        [Required(ErrorMessage = "Zip code is required.")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public string City { get; set; }
    }
}