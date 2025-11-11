using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.interfaces;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    /// <summary>
    /// Repository responsible for performing CRUD operations on <see cref="TodoItem"/> entities
    /// using an <see cref="AppDbContext"/> instance.
    /// </summary>
    /// <remarks>
    /// This repository implements <c>ITodoItemRepository</c> and exposes asynchronous methods
    /// to query, add, update and delete todo items. Most mutating operations (create, update,
    /// delete) only modify the EF Core change tracker; callers must invoke <see cref="SaveTodoItemAsync"/>
    /// to persist changes to the database.
    /// </remarks>
    /// <param name="context">The application <see cref="AppDbContext"/> used to access the data store.</param>
    public class TodoItemRepository(AppDbContext context) : ITodoItemRepository
    {
        /// <summary>
        /// Retrieves all todo items from the database asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an
        /// <see cref="IEnumerable{TodoItem}"/> with all todo items.
        /// </returns>
        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return await context.TodoItems.ToListAsync();
        }
        /// <summary>
        /// Retrieves a single <see cref="TodoItem"/> by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the todo item to retrieve.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the
        /// <see cref="TodoItem"/> if found; otherwise, <c>null</c>.
        /// </returns>
        public async Task<TodoItem?> GetTodoItemByIdAsync(int id)
        {
            return await context.TodoItems.FindAsync(id);
        }
        /// <summary>
        /// Adds a new <see cref="TodoItem"/> to the context asynchronously.
        /// </summary>
        /// <param name="todoItem">The todo item to add to the context.</param>
        /// <remarks>
        /// This method enqueues the entity in the EF Core change tracker but does not persist it
        /// to the database. Call <see cref="SaveTodoItemAsync"/> to commit the change.
        /// </remarks>
        /// <returns>A task that completes when the entity has been added to the change tracker.</returns>
        public async Task CreateTodoItemAsync(TodoItem todoItem)
        {
            await context.TodoItems.AddAsync(todoItem);
        }
        /// <summary>
        /// Persists all pending changes in the context to the database asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous save operation. The task completes when
        /// <see cref="AppDbContext.SaveChangesAsync"/> has finished.
        /// </returns>
        public Task SaveTodoItemAsync()
        {
            return context.SaveChangesAsync();
        }
        /// <summary>
        /// Updates an existing todo item by detaching the original tracked entity and marking the provided
        /// updated entity as modified.
        /// </summary>
        /// <param name="todoItem">The original tracked entity to detach.</param>
        /// <param name="updateTodoItem">The updated entity instance to mark as modified.</param>
        /// <remarks>
        /// This method modifies EF Core change tracking to replace the tracked instance. It does not
        /// call <see cref="SaveTodoItemAsync"/>; callers must invoke save to persist changes.
        /// Use caution with this approach as it bypasses automatic concurrency handling for the original instance.
        /// </remarks>
        /// <returns>A completed task.</returns>

        public Task UpdateTodoItemAsync(TodoItem todoItem, TodoItem updateTodoItem)
        {
            context.Entry(todoItem).State = EntityState.Detached;
            context.Entry(updateTodoItem).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        /// <summary>
        /// Removes the specified <see cref="TodoItem"/> from the context.
        /// </summary>
        /// <param name="todoItem">The todo item to remove.</param>
        /// <remarks>
        /// This method marks the entity for deletion in the EF Core change tracker. The deletion is not
        /// applied to the database until <see cref="SaveTodoItemAsync"/> is called.
        /// </remarks>
        /// <returns>A completed task.</returns>
        public Task DeleteTodoItemAsync(TodoItem todoItem)
        {
            context.TodoItems.Remove(todoItem);
            return Task.CompletedTask;
        }
    }
}