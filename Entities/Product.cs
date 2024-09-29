using System;
using System.Collections.Generic;
using Tamar_Sheva_Project;

namespace Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int Price { get; set; }

    public int? CategoryId { get; set; }

    public string? Description { get; set; }

    public string? ImgUrl { get; set; }

    public virtual Catgory? Category { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
   
}
