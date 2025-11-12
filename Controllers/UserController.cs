using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Dtos;
using ToDoList.interfaces;

namespace ToDoList.Controllers
{
    /// <summary>
    /// Handles HTTP requests for User management operations.
    /// Provides endpoints for creating, retrieving, updating, and deleting users.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService _userService) : ControllerBase
    {
        /// <summary>
        /// Creates a new user asynchronously.
        /// </summary>
        /// <param name="createUserDto">The data transfer object containing the user details to create.</param>
        /// <returns>
        /// A <see cref="CreatedAtActionResult"/> with status code 201 and the created todo item if successful.
        /// A <see cref="BadRequestResult"/> with status code 400 if an argument validation error occurs.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(createUserDto);
                return CreatedAtAction(
                    nameof(GetById),
                    new { username = createdUser.Username },
                    createdUser
                );
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        /// <summary>
        /// Retrieves all users asynchronously.
        /// </summary>
        /// <returns>
        /// An <see cref="OkResult"/> with status code 200 and a collection of all users.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }
        /// <summary>
        /// Retrieves a specific user by their username asynchronously.
        /// </summary>
        /// <param name="username">The unique username of the user to retrieve.</param>
        /// <returns>
        /// An <see cref="OkResult"/> with status code 200 and the user if found.
        /// A <see cref="NotFoundResult"/> with status code 404 if the user is not found.
        /// </returns>
        [HttpGet("{username}")]
        public async Task<IActionResult> GetById(string username)
        {
            try
            {
                var user = await _userService.GetUserByUsernameAsync(username);
                return Ok(user);
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        /// <summary>
        /// Updates an existing user asynchronously.
        /// </summary>
        /// <param name="username">The unique username of the user to update.</param>
        /// <param name="updateUserDto">The data transfer object containing the updated user details.</param>
        /// <returns>
        /// An <see cref="OkResult"/> with status code 200 and the updated user if successful.
        /// A <see cref="NotFoundResult"/> with status code 404 if the user is not found.
        /// A <see cref="BadRequestResult"/> with status code 400 if an argument validation error occurs.
        /// </returns>
        [HttpPut("{username}")]
        public async Task<IActionResult> Update(string username, [FromBody] UpdateUserDto updateUserDto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(username, updateUserDto);
                return Ok(updatedUser);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        /// <summary>
        /// Deletes an existing user asynchronously.
        /// </summary>
        /// <param name="username">The unique username of the user to delete.</param>
        /// <returns>
        /// A <see cref="NoContentResult"/> with status code 204 if deletion is successful.
        /// A <see cref="BadRequestResult"/> with status code 400 if an argument validation error occurs.
        /// </returns>
        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            try
            {
                await _userService.DeleteUserAsync(username);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}