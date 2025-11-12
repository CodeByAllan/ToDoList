using Microsoft.AspNetCore.Mvc;
using ToDoList.Dtos;
using ToDoList.interfaces;

namespace ToDoList.Controllers
{
    /// <summary>
    /// Handles HTTP requests for Todo Item management operations.
    /// Provides endpoints for creating, retrieving, updating, and deleting todo items.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TodoItemController(ITodoItemService _service) : ControllerBase

    {
        /// <summary>
        /// Creates a new todo item asynchronously.
        /// </summary>
        /// <param name="createTodoItemDto">The data transfer object containing the todo item details to create.</param>
        /// <returns>
        /// A <see cref="CreatedAtActionResult"/> with status code 201 and the created todo item if successful.
        /// A <see cref="BadRequestResult"/> with status code 400 if an argument validation error occurs.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTodoItemDto createTodoItemDto)
        {
            try
            {
                var createTodoItem = await _service.CreateTodoItemAsync(createTodoItemDto);
                return CreatedAtAction(
                    nameof(GetById),

                    new { id = createTodoItem.Id },
                    createTodoItem
                );

            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        /// <summary>
        /// Retrieves all todo items asynchronously.
        /// </summary>
        /// <returns>
        /// An <see cref="OkResult"/> with status code 200 and a collection of all todo items.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _service.GetTodoItemsAsync();
            return Ok(tasks);
        }
        /// <summary>
        /// Retrieves a specific todo item by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the todo item to retrieve.</param>
        /// <returns>
        /// An <see cref="OkResult"/> with status code 200 and the todo item if found.
        /// A <see cref="NotFoundResult"/> with status code 404 if the todo item is not found.
        /// </returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _service.GetTodoItemByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        /// <summary>
        /// Updates an existing todo item asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the todo item to update.</param>
        /// <param name="updateTodoItemDto">The data transfer object containing the updated todo item details.</param>
        /// <returns>
        /// An <see cref="OkResult"/> with status code 200 and the updated todo item if successful.
        /// A <see cref="BadRequestResult"/> with status code 400 if the request body is null or validation fails.
        /// A <see cref="NotFoundResult"/> with status code 404 if the todo item is not found.
        /// </returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTodoItemDto? updateTodoItemDto)
        {
            if (updateTodoItemDto == null)
            {
                return BadRequest(new { message = "The body of the request is required." });
            }
            try
            {
                var updatedTodoItem = await _service.UpdateTodoItemAsync(id, updateTodoItemDto);
                return Ok(updatedTodoItem);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        /// <summary>
        /// Deletes a todo item by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the todo item to delete.</param>
        /// <returns>
        /// A <see cref="NoContentResult"/> with status code 204 if deletion is successful.
        /// A <see cref="NotFoundResult"/> with status code 404 if the todo item is not found.
        /// A <see cref="BadRequestResult"/> with status code 400 if a validation error occurs.
        /// </returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteTodoItemAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}