using System;

namespace TaskManagementWebApp.Models
{
    public class ProjectTask
    {
        public int ProjectTaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}

