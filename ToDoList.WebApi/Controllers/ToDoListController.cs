using Microsoft.AspNetCore.Mvc;
using ToDoList.Application;
using ToDoList.Application.Models;
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

    [HttpGet]
    public IActionResult GetAll()
    {
        var items = _todoList.GetAllItems();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var item = _todoList.GetItem(id);
        if (item == null)
            return NotFound("Item not found.");

        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateRequest request)
    {
        var id = _repository.GetNextId();
        try
        {
            _todoList.AddItem(request.Id, request.Title, request.Description, request.Category);
            return CreatedAtAction(nameof(Create), new { id }, request);
        }
        catch (Exception ex)
        {
            // Log error
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateRequest request)
    {
        try
        {
            _todoList.UpdateItem(id, request.Description);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            _todoList.RemoveItem(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id}/progression")]
    public IActionResult RegisterProgression(int id, [FromBody] RegisterProgressionRequest progression)
    {
        try
        {
            _todoList.RegisterProgression(id, progression.Date, progression.Percentage);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("print")]
    public IActionResult Print()
    {
        try
        {
            _todoList.PrintItems(); // writes to console
            return Ok("Items printed to console.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}