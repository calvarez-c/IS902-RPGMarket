using Microsoft.AspNetCore.Mvc;
using RPGMarket.Application.Interfaces;
using RPGMarket.Domain.Entities;

namespace RPGMarket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemRepository _repository;

    public ItemsController(IItemRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetItems()
    {
        var items = await _repository.GetAllAsync();
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> CrearItem([FromBody] Item item)
    {
        if (string.IsNullOrWhiteSpace(item.Name))
            return BadRequest("El nombre del objeto es obligatorio.");

        var nuevoItem = await _repository.AddAsync(item);
        return CreatedAtAction(nameof(GetItems), new { id = nuevoItem.Id }, nuevoItem);
    }
}
