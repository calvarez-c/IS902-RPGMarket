namespace RPGMarket.Domain.Entities;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // "Arma", "Armadura", "Consumible", "Material"  
    public string Rarity { get; set; } = string.Empty; // "Común", "Raro", "Épico", "Legendario" 
    public decimal BasePrice { get; set; }
    public string? ImageUrl { get; set; }
}