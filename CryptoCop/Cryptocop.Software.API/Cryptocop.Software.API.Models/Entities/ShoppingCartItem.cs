﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cryptocop.Software.API.Models.Entities
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public string ProductIdentifier { get; set; }
        public float Quantity { get; set; }
        public float UnitPrice { get; set; }

        // Navigation properties
        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }

    }
}
