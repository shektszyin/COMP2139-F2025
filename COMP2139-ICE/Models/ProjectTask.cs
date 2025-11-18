namespace COMP2139_ICE.Models
{
    public class ProjectTask
    {
        public int ProjectTaskId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }
        public int ProjectId { get; set; }     
        public required Project Project { get; set; }   
    }
}
