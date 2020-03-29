using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Shared
{
    public abstract class AuditedEntity
    {
        public DateTime CreateDate { get; set; }
        [Required]
        public string CreateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        [Required]
        public string UpdateUserId { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string DeleteUserId { get; set; }
        public bool Deleted { get; set; }
    }
}
