using ToDoList.Dtos;
using ToDoList.interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    /// <summary>
    /// Service responsible for managing todo items.
    /// </summary>
    /// <remarks>
    /// Implements business logic on top of an <see cref="ITodoItemRepository"/>:
    /// - Validates input DTOs,
    /// - Creates, updates, deletes and queries <see cref="TodoItem"/> instances,
    /// - Persists changes by invoking repository save operations.
    /// All operations are asynchronous.
    /// </remarks>
    public class TodoItemService(ITodoItemRepository _repository) : ITodoItemService
    {
        /// <summary>
        /// Retrieves all todo items.
        /// </summary>
        /// <returns>
        /// A task that resolves to an <see cref="IEnumerable{TodoItem}"/> containing all todo items.
        /// </returns>

        public async Task<IEnumerable<TodoItem>> GetTodoItemsAsync()
        {
            return await _repository.GetTodoItemsAsync();
        }
        /// <summary>
        /// Retrieves a todo item by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the todo item to retrieve.</param>
        /// <returns>
        /// A task that resolves to the found <see cref="TodoItem"/>, or <c>null</c> if no item with the specified id exists.
        /// </returns>
        public async Task<TodoItem?> GetTodoItemByIdAsync(int id)
        {
            return await _repository.GetTodoItemByIdAsync(id);
        }
        /// <summary>
        /// Creates a new todo item from the provided DTO, sets its creation and update timestamps to UTC now,
        /// persists it using the repository, and returns the created entity.
        /// </summary>
        /// <param name="createTodoItemDto">DTO containing the data for the new todo item.</param>
        /// <returns>A task that resolves to the created <see cref="TodoItem"/>.</returns>
        /// <exception cref="ArgumentException">Thrown when <see cref="CreateTodoItemDto.Title"/> is null, empty, or whitespace.</exception>
        public async Task<TodoItem> CreateTodoItemAsync(CreateTodoItemDto createTodoItemDto)
        {
            if (string.IsNullOrWhiteSpace(createTodoItemDto.Title))
            {
                throw new ArgumentException("Title is required!");
            }
            var dateTimeNow = DateTime.UtcNow;
            var todoItem = new TodoItem
            {
                Title = createTodoItemDto.Title,
                Description = createTodoItemDto.Description,
                CreatedAt = dateTimeNow,
                UpdatedAt = dateTimeNow

            };
            await _repository.CreateTodoItemAsync(todoItem);
            await _repository.SaveTodoItemAsync();
            return todoItem;
        }
        /// <summary>
        /// Updates an existing todo item identified by <paramref name="id"/> using values from the provided DTO.
        /// Only non-null DTO properties are applied; <see cref="TodoItem.UpdatedAt"/> is set to UTC now.
        /// The repository is used to persist the changes.
        /// </summary>
        /// <param name="id">The identifier of the todo item to update.</param>
        /// <param name="updateTodoItemDto">DTO containing the updated values.</param>
        /// <returns>A task that resolves to the updated <see cref="TodoItem"/>.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no todo item with the specified id exists.</exception>
        public async Task<TodoItem> UpdateTodoItemAsync(int id, UpdateTodoItemDto updateTodoItemDto)
        {
            var todoItemIsExist = await _repository.GetTodoItemByIdAsync(id);
            if (todoItemIsExist == null)
            {
                throw new KeyNotFoundException($"TodoItem with Id {id} not found!");
            }
            var updatedTodoItem = todoItemIsExist with
            {
                Title = updateTodoItemDto.Title ?? todoItemIsExist.Title,
                Description = updateTodoItemDto.Description ?? todoItemIsExist.Description,
                IsCompleted = updateTodoItemDto.IsCompleted ?? todoItemIsExist.IsCompleted,
                UpdatedAt = DateTime.UtcNow

            };
            await _repository.UpdateTodoItemAsync(todoItemIsExist, updatedTodoItem);
            await _repository.SaveTodoItemAsync();
            return updatedTodoItem;
        }
        /// <summary>
        /// Deletes the todo item with the specified identifier and persists the removal via the repository.
        /// </summary>
        /// <param name="id">The identifier of the todo item to delete.</param>
        /// <returns>A task that completes when the delete operation and persistence have finished.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when no todo item with the specified id exists.</exception>
        public async Task DeleteTodoItemAsync(int id)
        {
            var todoItemExist = await _repository.GetTodoItemByIdAsync(id);
            if (todoItemExist == null)
            {
                throw new KeyNotFoundException($"TodoItem with Id {id} not found!");
            }
            await _repository.DeleteTodoItemAsync(todoItemExist);
            await _repository.SaveTodoItemAsync();
        }
    }
}

