namespace TaskManagementWebApp.Models
{
    public class ProjectTask
    {
        public int ProjectTaskId { get; set; } // Primary key
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
    }
}
