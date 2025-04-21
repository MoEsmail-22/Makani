using System;
using System.Collections.Generic;

namespace Graduation_Project.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Phone { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;
}
