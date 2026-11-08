namespace RPGMarket.Domain.Entities;

 public class Listing { 
    public int Id { get; set; } 
    public int SellerId { get; set; } 
    public User? Seller { get; set; } 
    public int? BuyerId { get; set; }
    public User? Buyer { get; set; } 
    public int ItemId { get; set; } 
    public Item? Item { get; set; } 
    public decimal Price { get; set; } 
    public string Status { get; set; } = "Active"; // "Active", "Sold", "Cancelled" 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
}