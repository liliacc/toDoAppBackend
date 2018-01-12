using Microsoft.EntityFrameworkCore;

namespace toDoAppBackend.Entities
{
    public class ToDoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDo> ToDos { get; set; }

        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=toDo.sqlite");
        }
    }
}
