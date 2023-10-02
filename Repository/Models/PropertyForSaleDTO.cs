public class PropertyForSaleDTO
{
    public int ID { get; set; }
    public string? OwnerEmail { get; set; }
    public string? Phone { get; set; }
    public string? Title { get; set; }
    public string? Location { get; set; }
    public string? Area { get; set; }
    public string? Description { get; set; }
    public Nullable<int> Price { get; set; }
    public Nullable<decimal> Latitude { get; set; }
    public Nullable<decimal> Longitude { get; set; }
}