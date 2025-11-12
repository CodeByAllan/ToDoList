using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data
{
    /// <summary>
    /// Represents the application's Entity Framework Core database context.
    /// Encapsulates configuration and DbSet properties used to persist ToDoList entities.
    /// </summary>
    /// <remarks>
    /// This context is typically configured with DbContextOptions&lt;AppDbContext&gt; provided via dependency injection.
    /// Configure the database provider and connection details through those options and use EF Core migrations to evolve the schema.
    /// </remarks>
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Gets or sets the collection of <see cref="TodoItem"/> entities.
        /// Provides CRUD access to the TodoItem table in the database.
        /// </summary>
        public DbSet<TodoItem> TodoItems { get; set; } = default!;
        /// <summary>
        /// Gets or sets the collection of <see cref="User"/> entities.
        /// Provides CRUD access to the User table in the database.
        /// </summary>
        public DbSet<User> Users { get; set; } = default!;

    }

}