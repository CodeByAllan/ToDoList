using ToDoList.Models;

namespace ToDoList.interfaces
{
    /// <summary>
    /// Defines the contract for data access operations on TodoItem entities.
    /// </summary>
    public interface ITodoItemRepository
    {
        /// <summary>
        /// Retrieves all todo items asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all TodoItem objects.</returns>
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync();

        /// <summary>
        /// Retrieves a specific todo item by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the todo item to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the TodoItem if found; otherwise, null.</returns>
        Task<TodoItem?> GetTodoItemByIdAsync(int id);

        /// <summary>
        /// Creates a new todo item asynchronously.
        /// </summary>
        /// <param name="todoItem">The TodoItem object to be created.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task CreateTodoItemAsync(TodoItem todoItem);

        /// <summary>
        /// Saves changes to the data store asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SaveTodoItemAsync();

        /// <summary>
        /// Updates an existing todo item with new values asynchronously.
        /// </summary>
        /// <param name="todoItem">The original TodoItem object to be updated.</param>
        /// <param name="updateTodoItem">The TodoItem object containing the updated values.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateTodoItemAsync(TodoItem todoItem, TodoItem updateTodoItem);

        /// <summary>
        /// Deletes a todo item asynchronously.
        /// </summary>
        /// <param name="todoItem">The TodoItem object to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteTodoItemAsync(TodoItem todoItem);
    }

}