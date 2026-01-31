namespace HhsCaseTracker.Api.Models;
using System.ComponentModel.DataAnnotations;

    public class Case
    {
        public int CaseId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Department { get; set; } = string.Empty;
        [Required]
        [RegularExpression("Open|In Progress|Resolved", 
        ErrorMessage = "Status must be Open, In Progress, or Resolved")]
        public string Status { get; set; } = "Open";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

