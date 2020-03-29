using Domain.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Products
{
    public class Product : AuditedEntity
    {
        public Product()
        {
            Id = new Guid();
        }

        public Guid Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
