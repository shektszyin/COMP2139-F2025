using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2139_ICE.Areas.ProjectManagement.Models
{
    public class ProjectComment
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        [StringLength(500)]
        public string Content { get; set; }

        // Foreign Key
        public int ProjectId { get; set; }

        // Navigation property
        public Project Project { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
