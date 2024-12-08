namespace TaskTracker.Models
{
    public record Tasks
    {
        public int Id { get; init; }
        public required string Description { get; set; }
        public Status Status { get; set; } = Status.todo;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
