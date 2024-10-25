namespace CodeTestApi.Domain.Entities;
public class Vehicle
{
    public required string Id { get; set; }
    public required string Brand { get; set; }
    public required string Model { get; set; }
    public DateTime ManufactureDate { get; set; }
    public bool IsAvailable { get; set; }
    public string? RentedBy { get; set; }
}
