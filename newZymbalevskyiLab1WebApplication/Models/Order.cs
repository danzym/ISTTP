using System;
using System.Collections.Generic;

namespace newZymbalevskyiLab1WebApplication;

public partial class Order
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public TimeSpan Time { get; set; }

    public int ProductId { get; set; }

    public int ProductQuantity { get; set; }

    public decimal ProductPrice { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
