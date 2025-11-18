using ToDoList.Dtos;
using ToDoList.Models;

namespace ToDoList.interfaces
{
    /// <summary>
    /// Defines the contract for TodoItem service operations.
    /// </summary>
    public interface ITodoItemService
    {
        /// <summary>
        /// Retrieves all TodoItems asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of TodoItem objects.</returns>
        Task<IEnumerable<TodoItem>> GetTodoItemsAsync(int userId);

        /// <summary>
        /// Retrieves a TodoItem by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the TodoItem to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the TodoItem if found; otherwise, null.</returns>
        Task<TodoItem?> GetTodoItemByIdAsync(int id, int userId);

        /// <summary>
        /// Creates a new TodoItem asynchronously.
        /// </summary>
        /// <param name="createTodoItemDto">The data transfer object containing the TodoItem creation details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the newly created TodoItem.</returns>
        Task<TodoItem> CreateTodoItemAsync(CreateTodoItemDto createTodoItemDto, int userId);

        /// <summary>
        /// Updates an existing TodoItem asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the TodoItem to update.</param>
        /// <param name="updateTodoItemDto">The data transfer object containing the TodoItem update details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated TodoItem.</returns>
        Task<TodoItem> UpdateTodoItemAsync(int id, UpdateTodoItemDto updateTodoItemDto, int userId);

        /// <summary>
        /// Deletes a TodoItem by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the TodoItem to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteTodoItemAsync(int id, int userId);
    }
}