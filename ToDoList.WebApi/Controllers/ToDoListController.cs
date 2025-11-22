using Microsoft.AspNetCore.Mvc;
using ToDoList.Application.Models;
using ToDoList.Application.Services;

namespace ToDoList.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToDoListController : ControllerBase
{
    private readonly TodoListApplicationService _applicationService;

    public ToDoListController(TodoListApplicationService applicationService)
    {
        _applicationService = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var items = _applicationService.GetAllItems();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        try
        {
            var item = _applicationService.GetItem(id);
            return Ok(item);
        }
        catch (Exception) // Should catch specific NotFound exception if mapped
        {
            return NotFound("Item not found.");
        }
    }

    [HttpGet("categories")]
    public IActionResult GetCategories()
    {
        var categories = _applicationService.GetCategories();
        return Ok(categories);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateRequest request)
    {
        try
        {
            var createdItem = _applicationService.AddItem(request.Title, request.Description, request.Category);
            return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateRequest request)
    {
        try
        {
            var updatedItem = _applicationService.UpdateItem(id, request.Description);
            return Ok(updatedItem);
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
            _applicationService.RemoveItem(id);
            return NoContent();
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
            var updatedItem = _applicationService.RegisterProgression(id, progression.Date, progression.Percentage);
            return Ok(updatedItem);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}