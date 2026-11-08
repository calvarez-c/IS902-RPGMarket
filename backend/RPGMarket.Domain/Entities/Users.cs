namespace RPGMarket.Domain.Entities;

public class User
    {
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty; public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty; public decimal Balance { get; set; } = 1000.00m;
    public string Role { get; set; } = "Player"; //"Player","Admin"
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}