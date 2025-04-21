using System;
using System.Collections.Generic;

namespace Graduation_Project.Models;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public decimal CommissionAmount { get; set; }

    public DateOnly Date { get; set; }

    public int CustomerId { get; set; }

    public int HouseId { get; set; }

    public int OwnerId { get; set; }

    public int EmployeeId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;

    public virtual House House { get; set; } = null!;
}
