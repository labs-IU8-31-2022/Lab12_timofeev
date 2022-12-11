using System;
using System.Collections.Generic;

namespace Razor.Entities;

public partial class Group
{
    public int GroupId { get; set; }

    public string? GroupName { get; set; }

    public virtual ICollection<Student> Students { get; } = new List<Student>();
}
