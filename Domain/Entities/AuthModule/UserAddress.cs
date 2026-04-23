namespace Domain.Entities.AuthModules;

public class UserAddress 
{
    public Guid Id { get; set; }

    public string Street { get; set; } = string.Empty;
    public string BuildingNumber { get; set; }
    public string Floor { get; set; }
    public string Apartment { get; set; }

    public string UserId { get; set; }

    public User User { get; set; }
}