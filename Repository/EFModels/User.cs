using Repository.EFModels;
using System;
using System.Collections.Generic;

namespace Repository.EFModels;

public partial class User
{
    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<PropertyForSale> PropertyForSales { get; set; } = new List<PropertyForSale>();
}
