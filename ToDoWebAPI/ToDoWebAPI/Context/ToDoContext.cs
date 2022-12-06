using Microsoft.EntityFrameworkCore;
using ToDoWebAPI.Entities;

namespace ToDoWebAPI.Context
{
    public class ToDoContext :DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) :base (options)
        {

        }
        public DbSet<ToDoEntity> ToDos => Set<ToDoEntity>();

    }
}
