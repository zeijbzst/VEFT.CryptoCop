using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptocop.Software.API.Models.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string StreetName { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string CardholderName { get; set; }
        public string CreditCard { get; set; }
        public string OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }

    }
}