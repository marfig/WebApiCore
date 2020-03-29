using Domain.Shared;
using System.ComponentModel.DataAnnotations;

namespace Domain.Products
{
    public class Category: AuditedEntity
    {
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
    }
}
