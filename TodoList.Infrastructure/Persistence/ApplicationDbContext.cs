using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Entities;
namespace TodoList.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() : base() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public DbSet<TodoItem> TodoItems { get; set; }
}