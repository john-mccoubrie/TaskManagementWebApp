using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementWebApp.Models
{
    public class ProjectTask
    {
        public int ProjectTaskId { get; set; } // Primary key
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty; 
        public DateTime DueDate { get; set; }

        //Properties for task assignment and status
        public string? AssignedUserId { get; set; }
        public TaskStatus Status { get; set; }

        //Navigation property for assigned user
        [ForeignKey(nameof(AssignedUserId))]
        public virtual IdentityUser? AssignedUser { get; set; }

        public enum TaskStatus
        {
            [Display(Name = "New Task")]
            New,
            [Display(Name = "In Progress")]
            InProgress,
            [Display(Name = "Completed")]
            Completed
        }
    }
}
