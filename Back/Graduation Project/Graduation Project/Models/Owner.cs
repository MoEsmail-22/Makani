using System;
using System.Collections.Generic;

namespace Graduation_Project.Models;

public partial class Owner
{
    public int OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<House> Houses { get; set; } = new List<House>();

    public virtual User User { get; set; } = null!;
}
