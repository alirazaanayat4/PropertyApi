using System;
using System.Collections.Generic;

namespace Repository.EFModels;

public partial class PropertyForSale
{
    public int Id { get; set; }

    public string OwnerEmail { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Title { get; set; }

    public string? Location { get; set; }

    public string? Area { get; set; }

    public string? Description { get; set; }

    public int? Price { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public virtual User OwnerEmailNavigation { get; set; } = null!;
}
