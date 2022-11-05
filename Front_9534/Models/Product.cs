using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Front_9534.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Category ProductCategory { get; set; }
        public int ProductCategoryId { get; set; }

        public Product(string name, string description, decimal price, int productCategoryId)
        {
            Name = name;
            Description = description;
            Price = price;
            ProductCategoryId = productCategoryId;
        }
    }
}