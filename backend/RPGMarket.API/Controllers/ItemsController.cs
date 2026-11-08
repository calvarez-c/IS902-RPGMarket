using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RPGMarket.Application.Features.Items.DTOs;
using RPGMarket.Application.Interfaces;
using RPGMarket.Domain.Entities;

namespace RPGMarket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemRepository _repository;
    private readonly IValidator<CreateItemRequestDto> _validator;
    private readonly IValidator<UpdateItemRequestDto> _updateValidator;

    public ItemsController(
        IItemRepository repository, 
        IValidator<CreateItemRequestDto> validator,
        IValidator<UpdateItemRequestDto> updateValidator
        )
    {
        _repository = repository;
        _validator = validator;
        _updateValidator = updateValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetItems()
    {
        var items = await _repository.GetAllAsync();
        var response = items.Select(i => new ItemResponseDto
        {
            Id = i.Id,
            Name = i.Name,
            Category = i.Category,
            Rarity = i.Rarity,
            BasePrice = i.BasePrice,
            ImageUrl = i.ImageUrl
        });
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetItemById(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
            return NotFound(new { mensaje = $"Objeto con ID {id} no fue encontrado." });

        var response = new ItemResponseDto
        {
            Id = item.Id,
            Name = item.Name,
            Category = item.Category,
            Rarity = item.Rarity,
            BasePrice = item.BasePrice,
            ImageUrl = item.ImageUrl
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CrearItem([FromBody] CreateItemRequestDto request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var nuevoItem = new Item
        {
            Name = request.Name,
            Category = request.Category,
            Rarity = request.Rarity,
            BasePrice = request.BasePrice,
            ImageUrl = request.ImageUrl
        };

        var itemCreado = await _repository.AddAsync(nuevoItem);

        var response = new ItemResponseDto
        {
            Id = itemCreado.Id,
            Name = itemCreado.Name,
            Category = itemCreado.Category,
            Rarity = itemCreado.Rarity,
            BasePrice = itemCreado.BasePrice,
            ImageUrl = itemCreado.ImageUrl
        };

        return CreatedAtAction(nameof(GetItemById), new { id = response.Id }, response);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateItemRequestDto request)
    {
        // 1. Validar con FluentValidation -> 400 Bad Request si falla
        var validationResult = await _updateValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        // 2. Buscar si la entidad existe
        var existingItem = await _repository.GetByIdAsync(id);
        if (existingItem == null)
            return NotFound(); // 404 Not Found si no existe

        // 3. Mapear cambios permitidos
        existingItem.Name = request.Name;
        existingItem.Category = request.Category;
        existingItem.BasePrice = request.BasePrice;
        existingItem.Rarity = request.Rarity;
        existingItem.ImageUrl = request.ImageUrl;

        // 4. Persistir
        await _repository.UpdateAsync(existingItem);

        // 5. Semántica HTTP exigida: 204 No Content
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarItem(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
            return NotFound();

        await _repository.DeleteAsync(id);
        return NoContent(); // HTTP 204
    }
}
