using System;
using System.Collections.Generic;

namespace newZymbalevskyiLab1WebApplication;

public partial class Product
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public string Description { get; set; } = null!;

    public string? Manufacturer { get; set; }

    public string? ImageLink { get; set; }

    public int OrderId { get; set; }

    public int CategoryId { get; set; }

    public int SupplierId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
