using Microsoft.AspNetCore.Mvc;
using ToDoList.Application;
using ToDoList.Repository;

[ApiController]
[Route("api/[controller]")]
public class ToDoListController : ControllerBase
{
    private readonly IToDoList _todoList;
    private readonly ITodoListRepository _repository;

    public ToDoListController(IToDoList todoList, ITodoListRepository repository)
    {
        _todoList = todoList;
        _repository = repository;
    }

    [HttpPost]
    public IActionResult CreateTodo([FromBody] CreateTodoItemDto item)
    {
        // Typically, repo would provide a new ID and categories; you may inject that via a scoped service instead.
        var id = item.Id; // Replace with repo.GetNextId() if you inject the repo
        try
        {
            _todoList.AddItem(id, item.Title, item.Description, item.Category);
            return CreatedAtAction(nameof(GetTodo), new { id }, item);
        }
        catch (Exception ex)
        {
            // Log error
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetTodo(int id)
    {
        // Implement method to return a single todo item.
        // Example: FindById and map to DTO.
        return Ok();
    }

    [HttpPost("{id}/progression")]
    public IActionResult RegisterProgression(int id, [FromBody] RegisterProgressionDto progression)
    {
        try
        {
            _todoList.RegisterProgression(id, progression.DateTime, progression.Percent);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Additional endpoints: update, delete, print items, etc.
}

// DTOs (you may move these to a dedicated DTOs folder)
public record CreateTodoItemDto(int Id, string Title, string Description, string Category);
public record RegisterProgressionDto(DateTime DateTime, decimal Percent);
