namespace RPGMarket.Application.Features.Items.DTOs;

public class UpdateItemRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Rarity { get; set; } = string.Empty;
    public decimal BasePrice { get; set; }
    public string? ImageUrl { get; set; }
}