namespace tasks_api.Models
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool Concluded { get; set; }
        public int UserId { get; set; }
        public Usuario User { get; set; }
    }
}