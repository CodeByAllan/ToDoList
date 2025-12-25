using System.Security.Claims;
using TodoList.Application.Dtos;
using TodoList.Application.Interfaces;
using TodoList.Domain.Entities;
using ToDoList.Utils;

namespace TodoList.Api.Endpoints;

public static class TodoEndpoints
{
    public static void MapTodoEndpoints(this WebApplication app)
    {
        var todos = app.MapGroup("/todos").RequireAuthorization();

        todos.MapPost("/", async (ITodoItemService _service, CreateTodoItemDto request, ClaimsPrincipal claimsPrincipal) =>
        {
            try
            {
                TodoItem newItem = await _service.CreateAsync(request, claimsPrincipal.GetUserId());
                return Results.Created($"/todos/{newItem.ID}", newItem);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(new { message = ex.Message });
            }
        }).WithName("CreateTodoItem");

        todos.MapGet("/", async (ITodoItemService _service, ClaimsPrincipal claimsPrincipal) =>
        {
            try { return Results.Ok(await _service.GetAllAsync(claimsPrincipal.GetUserId())); }
            catch (Exception ex)
            {
                return Results.InternalServerError(new { message = ex.Message });
            }
        }).WithName("GetAllTodoItem");

        todos.MapGet("/{id}", async (ITodoItemService _service, int id, ClaimsPrincipal claimsPrincipal) =>
        {
            try
            {
                TodoItem? todoItem = await _service.GetByIdAsync(id, claimsPrincipal.GetUserId());
                if (todoItem != null)
                {
                    return Results.Ok(todoItem);
                }
                else
                {
                    return Results.NotFound();
                }
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(new { message = ex.Message });
            }
        });

        todos.MapPut("/{id}", async (ITodoItemService _service, int id, UpdateTodoItemDto request, ClaimsPrincipal claimsPrincipal) =>
        {
            try
            {
                TodoItem todoItem = await _service.UpdateAsync(id, request, claimsPrincipal.GetUserId());
                return Results.Ok(todoItem);
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(new { message = ex.Message });
            }
        });
        todos.MapDelete("/{id}", async (ITodoItemService _service, int id, ClaimsPrincipal claimsPrincipal) =>
        {
            try
            {
                await _service.DeleteAsync(id, claimsPrincipal.GetUserId());
                return Results.NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return Results.NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return Results.InternalServerError(new { message = ex.Message });
            }
        });
    }
}