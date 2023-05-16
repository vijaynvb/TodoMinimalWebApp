using TodoMinimalWebApp.Models;

namespace TodoMinimalWebApp.Data.Repositories;

public interface ITodosRepository
{
    Task<List<Todo>> GetAllTodos(string token);
    Task<Todo?> GetTodoById(int todoId);
    Task<Todo?> CreateTodo(Todo newTodo);
    Task DeleteTodo(int todoId);
    Task<Todo?> UpdateTodo(int todoId, Todo updatedTodo);
}