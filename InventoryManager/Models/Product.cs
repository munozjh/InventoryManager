using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace InventoryManager.Models
{
    internal class Product
    {

        public String Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int Id { get; set; }

        public Product(string name, int quantity, decimal price, int id)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Id = id;
        }
    }
}
