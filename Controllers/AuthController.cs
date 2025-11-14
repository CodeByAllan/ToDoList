using Microsoft.AspNetCore.Mvc;
using ToDoList.Dtos;
using ToDoList.interfaces;

namespace ToDoList.Controllers
{
    /// <summary>
    /// Controller for handling authentication requests.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        /// <summary>
        /// Handles user login requests.
        /// </summary>
        /// <!----> <param name="loginRequest">The login request containing user credentials.</param>
        /// <returns>
        /// An <see cref="OkResult"/> with status code 200 and a JWT token if login is successful.
        /// An <see cref="UnauthorizedResult"/> with status code 401 if login fails.
        /// </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var token = await _authService.Login(loginRequestDto);
            if (token == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }
            return Ok(new { token });
        }

        /// <summary>
        /// Handles user registration requests.
        /// </summary>
        /// <param name="registerRequestDto">The registration request containing user details.</param>  
        /// <returns>
        /// An <see cref="OkResult"/> with status code 200 and a JWT token if registration is successful.
        /// A <see cref="NotFoundResult"/> with status code 404 if a required resource is not found.
        /// A <see cref="BadRequestResult"/> with status code 400 if the request is invalid.
        /// </returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            try
            {
                var token = await _authService.Register(registerRequestDto);
                return Ok(new { token });
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