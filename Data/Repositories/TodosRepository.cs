using System.Text;
using Newtonsoft.Json;
using TodoMinimalWebApp.Models;

namespace TodoMinimalWebApp.Data.Repositories;

public class TodosRepository : ITodosRepository
{
    private readonly HttpClient _httpClient;

    public TodosRepository()
    {
        _httpClient = new HttpClient();
        // jsonplaceholder.typicode server
        //_httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");
        // Local server
        _httpClient.BaseAddress = new Uri("http://localhost:5283");
    }

    public async Task<Todo?> CreateTodo(Todo newTodo)
    {
        var newTodoAsString = JsonConvert.SerializeObject(newTodo);
        var responseBody = new StringContent(newTodoAsString, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("/todos", responseBody);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var todo = JsonConvert.DeserializeObject<Todo>(content);
            return todo;
        }

        return null;
    }

    public async Task DeleteTodo(int todoId)
    {
        var response = await _httpClient.DeleteAsync($"/todos/{todoId}");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsByteArrayAsync();
            Console.WriteLine("Delete Todo Response: ", data);
        }
    }

    public async Task<List<Todo>> GetAllTodos()
    {
        var response = await _httpClient.GetAsync("/todos");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var todos = JsonConvert.DeserializeObject<List<Todo>>(content);
            return todos ?? new();
        }

        return new();
    }

    public async Task<Todo?> GetTodoById(int todoId)
    {
        var response = await _httpClient.GetAsync($"/todos/{todoId}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var todo = JsonConvert.DeserializeObject<Todo>(content);
            return todo;
        }

        return null;
    }

    public async Task<Todo?> UpdateTodo(int todoId, Todo updatedTodo)
    {
        var newTodoAsString = JsonConvert.SerializeObject(updatedTodo);
        var responseBody = new StringContent(newTodoAsString, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"/todos/{todoId}", responseBody);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var todo = JsonConvert.DeserializeObject<Todo>(content);
            return todo;
        }

        return null;
    }
}