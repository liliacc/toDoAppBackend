namespace toDoAppBackend.Entities
{
    public class ToDo
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
    }
}
