using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.interfaces;
using ToDoList.Models;

namespace ToDoList.Repositories
{
    /// <summary>
    /// Repository responsible for performing CRUD operations on <see cref="User"/> entities
    /// using an <see cref="AppDbContext"/> instance.
    /// </summary>
    /// <remarks>
    /// This repository implements <c>IUserRepository</c> and exposes asynchronous methods
    /// to query, add, update and delete todo items. Most mutating operations (create, update,
    /// delete) only modify the EF Core change tracker; callers must invoke <see cref="SaveUserAsync"/>
    /// to persist changes to the database.
    /// </remarks>
    /// <param name="context">The application <see cref="AppDbContext"/> used to access the data store.</param>
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        /// <summary>
        /// Asynchronously creates a new <see cref="User"/> entity in the data store.
        /// </summary>
        /// <param name="user">The <see cref="User"/> entity to create.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        /// <remarks>
        /// This method only modifies the EF Core change tracker; callers must invoke
        /// <see cref="SaveUserAsync"/> to persist changes to the database.
        /// </remarks>
        async Task IUserRepository.CreateUserAsync(User user)
        {
            await context.Users.AddAsync(user);
        }
        /// <summary>
        /// Asynchronously deletes an existing <see cref="User"/> entity from the data store.
        /// </summary>
        /// <param name="user">The <see cref="User"/> entity to delete.</param>
        /// <returns>
        /// A <see cref="Task"/>
        /// representing the asynchronous operation.
        /// </returns>
        /// <remarks>
        /// This method only modifies the EF Core change tracker; callers must invoke
        /// <see cref="SaveUserAsync"/> to persist changes to the database.
        /// </remarks>
        Task IUserRepository.DeleteUserAsync(User user)
        {
            context.Users.Remove(user);
            return Task.CompletedTask;
        }
       /// <summary>
        /// Asynchronously retrieves a <see cref="User"/> entity by its username from the data store.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> representing the asynchronous operation,
        /// containing the <see cref="User"/> entity if found; otherwise, <c>null
        /// </c>.
        /// </returns>
        /// <remarks>
        /// The username comparison is case-insensitive.
        /// </remarks>
        async Task<User?> IUserRepository.GetUserByUsernameAsync(string username)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());
        }
        /// <summary>
        /// Asynchronously retrieves all <see cref="User"/> entities from the data store.
        /// </summary>
        /// <returns>
        /// A <see cref="Task{TResult}"/> representing the asynchronous operation,
        /// containing an <see cref="IEnumerable{User}"/> of all users.
        /// </returns>
        /// <remarks>
        /// The users are returned in no particular order.
        /// </remarks>
        async Task<IEnumerable<User>> IUserRepository.GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }
        /// <summary>
        /// Asynchronously saves all changes made in the context to the data store.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        /// <remarks>
        /// This method must be called to persist any changes made by create, update,
        /// or delete operations.
        /// </remarks>
        Task IUserRepository.SaveUserAsync()
        {
            return context.SaveChangesAsync();
        }
        /// <summary>
        /// Asynchronously updates an existing <see cref="User"/> entity in the data store.
        ///    </summary>
        /// <param name="user">The existing <see cref="User"/> entity to update.</param>
        /// <param name="updateUser">The <see cref="User"/> entity containing updated values.</param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        /// <remarks>
        /// This method only modifies the EF Core change tracker; callers must invoke
        /// <see cref="SaveUserAsync"/> to persist changes to the database.
        /// </remarks>  
        Task IUserRepository.UpdateUserAsync(User user, User updateUser)
        {
            context.Entry(user).State = EntityState.Detached;
            context.Entry(updateUser).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}