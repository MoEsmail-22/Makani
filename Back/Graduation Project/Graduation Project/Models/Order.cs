using System;
using System.Collections.Generic;

namespace Graduation_Project.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public int HouseId { get; set; }

    public string? Message { get; set; }

    public virtual House House { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
