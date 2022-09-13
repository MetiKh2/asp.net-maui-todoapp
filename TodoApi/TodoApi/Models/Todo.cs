namespace TodoApi.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDone { get; set; }
    }
}
