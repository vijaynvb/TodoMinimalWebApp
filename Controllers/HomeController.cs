using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TodoMinimalWebApp.Data.Repositories;
using TodoMinimalWebApp.Models;

namespace TodoMinimalWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ITodosRepository _todosRepository;

    public HomeController(ILogger<HomeController> logger, ITodosRepository todosRepository)
    {
        _logger = logger;
        _todosRepository = todosRepository;
    }

    public async Task<IActionResult> Index()
    {
        var token = HttpContext.Session.GetString("JWToken");
        var viewModel = new IndexViewModel
        {
            Todos = await _todosRepository.GetAllTodos(token)
        };

        return View(viewModel);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Todo newTodo)
    {
        newTodo.UserId = 1;
        newTodo.IsCompleted = false;

        await _todosRepository.CreateTodo(newTodo);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int todoId)
    {
        var todo = await _todosRepository.GetTodoById(todoId);

        if (todo is null)
            return NotFound();

        return View(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Todo updatedTodo)
    {
        var test = await _todosRepository.UpdateTodo(updatedTodo.Id, updatedTodo);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int todoId)
    {
        await _todosRepository.DeleteTodo(todoId);
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
