using System.ComponentModel.DataAnnotations;

namespace COMP2139_ICE.Areas.ProjectManagement.Models
{
    public class ProjectTask
    {
        public int ProjectTaskId { get; set; }

        [StringLength(100)]
        [Display(Name = "Task Title")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(500)]
        [Display(Name = "Task Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }

        [Display(Name = "Completed?")]
        public bool IsCompleted { get; set; }

        // Foreign Key
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
