using System;
using System.Collections;
using System.Collections.Generic;

namespace Cryptocop.Software.API.Models.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; } // Linked to User with email.
        public string FullName { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string CardHolderName { get; set; }
        public string MaskedCreditCard { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public double TotalPrice { get; set; }

        // Navigation properties
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
