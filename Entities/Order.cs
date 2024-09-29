using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Tamar_Sheva_Project;

namespace Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public int? OrderSum { get; set; }

    public int? UserId { get; set; }
   
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User? User { get; set; }
}
