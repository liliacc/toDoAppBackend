using Microsoft.EntityFrameworkCore;

namespace toDoAppBackend.Entities
{
    public class ToDoDbContext : DbContext
    {
        public DbSet<ToDo> ToDos { get; set; }

        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }
    }
}
